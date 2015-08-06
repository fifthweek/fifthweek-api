namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    [AutoConstructor]
    public partial class ProcessAllPayments : IProcessAllPayments
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly IGetAllSubscribersDbStatement getAllSubscribers;
        private readonly IProcessPaymentsForSubscriber processPaymentsForSubscriber;
        private readonly IUpdateAccountBalancesDbStatement updateAccountBalances;
        private readonly ITopUpUserAccountsWithCredit topUpUserAccountsWithCredit;

        /// <summary>
        /// All ledger entries that decrease a user's account balance must be procesed here
        /// to ensure we don't end up with negative account balances due to race conditions.
        /// 
        /// This code runs serially, with a lock on a blob to ensure only one instance is running
        /// at a time.
        /// 
        /// It is possible that the user can manually add credit to their account while this is running.
        /// While this is unlikely, it is also not an issue.
        /// 
        /// The most important priority is that the COMMITTED account balance never goes negative.
        /// The committed account balance is the sum of all rows in the append-only ledger for an account,
        /// and represents real money the user has spend or recieved. If the committed balance goes negative
        /// then that implies the user has spent more than they have, and Fifthweek will have to make up the
        /// difference (which would be bad).
        /// 
        /// The (uncommitted) account balance that the user sees may go negative, and it is desirable 
        /// for it to do so as it means the user will understand that they will still be billed for 
        /// money tentatively owed if they top-up again. However if the uncommitted transaction is committed
        /// then it will be have to be adjusted by to ensure the committed balance does not go negative.
        /// 
        /// To do this, first read the committed account balance for a subscriber when we start processing
        /// that subscriber's payments.
        /// 
        /// When we commit new payments, subtract the amount from committed balance.
        /// If committed balance would be less than zero, adjust the committed payment to ensure account 
        /// balance is exactly zero.
        /// 
        /// This is the only viable option, other than Fifthweek covering the difference or chasing the user
        /// for funds. In reality, on the rare occasions this might occur it would be in the 
        /// order of $0.01.  Essentially, we accept that the user cannot pay the amount owed, and
        /// do the best we can.
        /// 
        /// Afterwards, commit any tips that wouldn't make committed balance less than zero. 
        /// Keep tips that can't be committed as uncommitted to be processed later if possible.
        /// A creator will not see uncommitted tips, but they will count towards the users 
        /// (uncommitted, posibly negative) account balance.  We can always remove old uncommitted 
        /// tips after a few weeks, to ensure we don't surprise bill the subscriber 2 years later when he
        /// logs in again.
        /// 
        /// Processing refunds:
        ///  - For a refund from a particular creator as credits, refund all uncommitted payments.
        ///     - Ensure this won't be recalculated or committed on next iteration....
        ///     - We could do this by unsubscribing the user and inserting a refund record
        ///       which would be taken into account by the payment processing algorithm to zero
        ///       payments in that week before the refund record.
        ///     - Creating a refund should be done at the start of payment processing, using the
        ///       processing timestamp, to ensure the refund is in an uncommitted week.
        ///
        ///  - For full refund to the user's bank account, refund all remaining balance by updating
        ///    ledger (transfer to refund account) and immediately update user's calculated 
        ///    balance (it should be zero).
        ///     - This must be done within this algorithm to avoid race conditions.
        ///     - Initiation could be done by the user through the website.
        ///     - It should be done at the start of this algorithm, to ensure the user gets the refund 
        ///       they expect.
        ///     - The refund amount, now recorded in the refund account, can then be manually or automatically
        ///       transferred to the user.
        ///     - Taxamo needs to be updated.
        ///     - Tax can also be refunded (moved from user's sales tax account to refund account).
        /// </summary>
        public async Task ExecuteAsync(IKeepAliveHandler keepAliveHandler, List<PaymentProcessingException> errors, CancellationToken cancellationToken)
        {
            PaymentsPerformanceLogger.Instance.Clear();
            using (PaymentsPerformanceLogger.Instance.Log(typeof(ProcessAllPayments)))
            {
                keepAliveHandler.AssertNotNull("keepAliveHandler");
                errors.AssertNotNull("errors");

                var subscriberIds = await this.getAllSubscribers.ExecuteAsync();

                // We use the same end time for all subscribers so that when we update
                // all account balances at the end the timestamp is accurate.
                var endTimeExclusive = this.timestampCreator.Now();

                foreach (var subscriberId in subscriberIds)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    using (PaymentsPerformanceLogger.Instance.Log("Subscriber"))
                    {
                        try
                        {
                            await this.processPaymentsForSubscriber.ExecuteAsync(
                                subscriberId,
                                endTimeExclusive,
                                keepAliveHandler,
                                errors);
                        }
                        catch (Exception t)
                        {
                            errors.Add(new PaymentProcessingException(t, subscriberId, null));
                        }
                    }
                }

                using (PaymentsPerformanceLogger.Instance.Log("BalancesAndCredit"))
                {
                    var updatedBalances = await this.updateAccountBalances.ExecuteAsync(null, endTimeExclusive);

                    bool recalculateBalances = await this.topUpUserAccountsWithCredit.ExecuteAsync(
                        updatedBalances, errors, cancellationToken);

                    if (recalculateBalances)
                    {
                        await this.updateAccountBalances.ExecuteAsync(null, endTimeExclusive);
                    }
                }
            }

            PaymentsPerformanceLogger.Instance.TraceResults();
        }
    }

    public class PaymentsPerformanceLogger
    {
        public static readonly PaymentsPerformanceLogger Instance = new PaymentsPerformanceLogger();

        private readonly Dictionary<string, Item> items = new Dictionary<string, Item>();
        
        private PaymentsPerformanceLogger()
        {
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public IDisposable Log(Type type)
        {
            return this.Log(type.Name);
        }

        public IDisposable Log(string name)
        {
            return new ItemLogger(name, this.items);
        }

        public void TraceResults()
        {
            foreach (var item in this.items.Values)
            {
                item.TraceResult();
            }
        }

        private class Item
        {
            private string name;

            private TimeSpan cumulativeTime;

            private int count;

            public Item(string name)
            {
                this.name = name;
            }

            public void Add(TimeSpan time)
            {
                this.cumulativeTime += time;
                ++this.count;
            }

            public void TraceResult()
            {
                var line = string.Format(
                    "{0}: {2} calls, {1}ms",
                    this.name,
                    this.cumulativeTime.TotalMilliseconds,
                    this.count);

                Trace.TraceInformation(line);
                Console.WriteLine(line);
            }
        }

        private class ItemLogger : IDisposable
        {
            private readonly string name;

            private readonly IDictionary<string, Item> items;

            private readonly Stopwatch stopwatch;

            public ItemLogger(string name, IDictionary<string, Item> items)
            {
                this.name = name;
                this.items = items;
                this.stopwatch = new Stopwatch();
                this.stopwatch.Start();
            }

            public void Dispose()
            {
                this.stopwatch.Stop();
                Item existingItem;
                if (!this.items.TryGetValue(this.name, out existingItem))
                {
                    existingItem = new Item(this.name);
                    this.items.Add(this.name, existingItem);
                }

                existingItem.Add(this.stopwatch.Elapsed);
            }
        }
    }
}