using System;
using System.Linq;

//// Generated on 25/06/2015 13:14:45 (UTC)
//// Mapped solution in 12.14s


namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Fifthweek.Payments.Pipeline;

    public partial class AggregateCostSummary 
    {
        public AggregateCostSummary(
            System.Decimal cost)
        {
            if (cost == null)
            {
                throw new ArgumentNullException("cost");
            }

            this.Cost = cost;
        }
    }
}
namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Payments.Services;

    public partial class CalculateCostPeriodsExecutor 
    {
        public CalculateCostPeriodsExecutor(
            Fifthweek.Payments.Pipeline.ICalculateSnapshotCostExecutor costCalculator)
        {
            if (costCalculator == null)
            {
                throw new ArgumentNullException("costCalculator");
            }

            this.costCalculator = costCalculator;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetAllCreatorsDbStatement 
    {
        public GetAllCreatorsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetAllSubscribedUsersDbStatement 
    {
        public GetAllSubscribedUsersDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetCreatorChannelsSnapshotsDbStatement 
    {
        public GetCreatorChannelsSnapshotsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetCreatorFreeAccessUsersSnapshotsDbStatement 
    {
        public GetCreatorFreeAccessUsersSnapshotsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetCreatorPostsDbStatement 
    {
        public GetCreatorPostsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetSubscriberChannelsSnapshotsDbStatement 
    {
        public GetSubscriberChannelsSnapshotsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetSubscriberSnapshotsDbStatement 
    {
        public GetSubscriberSnapshotsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Shared;

    public partial class ProcessAllPayments 
    {
        public ProcessAllPayments(
            Fifthweek.Shared.ITimestampCreator timestampCreator,
            Fifthweek.Payments.Services.IGetAllSubscribersDbStatement getAllSubscribers,
            Fifthweek.Payments.Services.IProcessPaymentsForSubscriber processPaymentsForSubscriber,
            Fifthweek.Payments.Services.IUpdateAccountBalancesDbStatement updateAccountBalances)
        {
            if (timestampCreator == null)
            {
                throw new ArgumentNullException("timestampCreator");
            }

            if (getAllSubscribers == null)
            {
                throw new ArgumentNullException("getAllSubscribers");
            }

            if (processPaymentsForSubscriber == null)
            {
                throw new ArgumentNullException("processPaymentsForSubscriber");
            }

            if (updateAccountBalances == null)
            {
                throw new ArgumentNullException("updateAccountBalances");
            }

            this.timestampCreator = timestampCreator;
            this.getAllSubscribers = getAllSubscribers;
            this.processPaymentsForSubscriber = processPaymentsForSubscriber;
            this.updateAccountBalances = updateAccountBalances;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class SubscriberPaymentPipeline 
    {
        public SubscriberPaymentPipeline(
            Fifthweek.Payments.Pipeline.IVerifySnapshotsExecutor verifySnapshots,
            Fifthweek.Payments.Pipeline.IMergeSnapshotsExecutor mergeSnapshots,
            Fifthweek.Payments.Pipeline.IRollBackSubscriptionsExecutor rollBackSubscriptions,
            Fifthweek.Payments.Pipeline.IRollForwardSubscriptionsExecutor rollForwardSubscriptions,
            Fifthweek.Payments.Pipeline.ITrimSnapshotsExecutor trimSnapshots,
            Fifthweek.Payments.Pipeline.IAddSnapshotsForBillingEndDatesExecutor addSnapshotsForBillingEndDates,
            Fifthweek.Payments.Pipeline.ICalculateCostPeriodsExecutor calculateCostPeriods,
            Fifthweek.Payments.Pipeline.IAggregateCostPeriodsExecutor aggregateCostPeriods)
        {
            if (verifySnapshots == null)
            {
                throw new ArgumentNullException("verifySnapshots");
            }

            if (mergeSnapshots == null)
            {
                throw new ArgumentNullException("mergeSnapshots");
            }

            if (rollBackSubscriptions == null)
            {
                throw new ArgumentNullException("rollBackSubscriptions");
            }

            if (rollForwardSubscriptions == null)
            {
                throw new ArgumentNullException("rollForwardSubscriptions");
            }

            if (trimSnapshots == null)
            {
                throw new ArgumentNullException("trimSnapshots");
            }

            if (addSnapshotsForBillingEndDates == null)
            {
                throw new ArgumentNullException("addSnapshotsForBillingEndDates");
            }

            if (calculateCostPeriods == null)
            {
                throw new ArgumentNullException("calculateCostPeriods");
            }

            if (aggregateCostPeriods == null)
            {
                throw new ArgumentNullException("aggregateCostPeriods");
            }

            this.verifySnapshots = verifySnapshots;
            this.mergeSnapshots = mergeSnapshots;
            this.rollBackSubscriptions = rollBackSubscriptions;
            this.rollForwardSubscriptions = rollForwardSubscriptions;
            this.trimSnapshots = trimSnapshots;
            this.addSnapshotsForBillingEndDates = addSnapshotsForBillingEndDates;
            this.calculateCostPeriods = calculateCostPeriods;
            this.aggregateCostPeriods = aggregateCostPeriods;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class CreatorChannelsSnapshot 
    {
        public CreatorChannelsSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorChannelsSnapshotItem> creatorChannels)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (creatorChannels == null)
            {
                throw new ArgumentNullException("creatorChannels");
            }

            this.Timestamp = timestamp;
            this.CreatorId = creatorId;
            this.CreatorChannels = creatorChannels;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class CreatorChannelsSnapshotItem 
    {
        public CreatorChannelsSnapshotItem(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.Int32 price)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (price == null)
            {
                throw new ArgumentNullException("price");
            }

            this.ChannelId = channelId;
            this.Price = price;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class CreatorFreeAccessUsersSnapshot 
    {
        public CreatorFreeAccessUsersSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.Collections.Generic.IReadOnlyList<System.String> freeAccessUserEmails)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (freeAccessUserEmails == null)
            {
                throw new ArgumentNullException("freeAccessUserEmails");
            }

            this.Timestamp = timestamp;
            this.CreatorId = creatorId;
            this.FreeAccessUserEmails = freeAccessUserEmails;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class MergedSnapshot 
    {
        public MergedSnapshot(
            System.DateTime timestamp,
            Fifthweek.Payments.Snapshots.CreatorChannelsSnapshot creatorChannels,
            Fifthweek.Payments.Snapshots.CreatorFreeAccessUsersSnapshot creatorFreeAccessUsers,
            Fifthweek.Payments.Snapshots.SubscriberChannelsSnapshot subscriberChannels,
            Fifthweek.Payments.Snapshots.SubscriberSnapshot subscriber,
            Fifthweek.Payments.Snapshots.CalculatedAccountBalanceSnapshot calculatedAccountBalance)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorChannels == null)
            {
                throw new ArgumentNullException("creatorChannels");
            }

            if (creatorFreeAccessUsers == null)
            {
                throw new ArgumentNullException("creatorFreeAccessUsers");
            }

            if (subscriberChannels == null)
            {
                throw new ArgumentNullException("subscriberChannels");
            }

            if (subscriber == null)
            {
                throw new ArgumentNullException("subscriber");
            }

            if (calculatedAccountBalance == null)
            {
                throw new ArgumentNullException("calculatedAccountBalance");
            }

            this.Timestamp = timestamp;
            this.CreatorChannels = creatorChannels;
            this.CreatorFreeAccessUsers = creatorFreeAccessUsers;
            this.SubscriberChannels = subscriberChannels;
            this.Subscriber = subscriber;
            this.CalculatedAccountBalance = calculatedAccountBalance;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class SubscriberChannelsSnapshot 
    {
        public SubscriberChannelsSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId subscriberId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberChannelsSnapshotItem> subscribedChannels)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            if (subscribedChannels == null)
            {
                throw new ArgumentNullException("subscribedChannels");
            }

            this.Timestamp = timestamp;
            this.SubscriberId = subscriberId;
            this.SubscribedChannels = subscribedChannels;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class SubscriberChannelsSnapshotItem 
    {
        public SubscriberChannelsSnapshotItem(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.Int32 acceptedPrice,
            System.DateTime subscriptionStartDate)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (acceptedPrice == null)
            {
                throw new ArgumentNullException("acceptedPrice");
            }

            if (subscriptionStartDate == null)
            {
                throw new ArgumentNullException("subscriptionStartDate");
            }

            this.ChannelId = channelId;
            this.AcceptedPrice = acceptedPrice;
            this.SubscriptionStartDate = subscriptionStartDate;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class SubscriberSnapshot 
    {
        public SubscriberSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId subscriberId,
            System.String email)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            this.Timestamp = timestamp;
            this.SubscriberId = subscriberId;
            this.Email = email;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CreatorPost 
    {
        public CreatorPost(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.DateTime liveDate)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (liveDate == null)
            {
                throw new ArgumentNullException("liveDate");
            }

            this.ChannelId = channelId;
            this.LiveDate = liveDate;
        }
    }
}
namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Payments.Services;

    public partial class CostPeriod 
    {
        public CostPeriod(
            System.DateTime startTimeInclusive,
            System.DateTime endTimeExclusive,
            System.Int32 cost)
        {
            if (startTimeInclusive == null)
            {
                throw new ArgumentNullException("startTimeInclusive");
            }

            if (endTimeExclusive == null)
            {
                throw new ArgumentNullException("endTimeExclusive");
            }

            if (cost == null)
            {
                throw new ArgumentNullException("cost");
            }

            this.StartTimeInclusive = startTimeInclusive;
            this.EndTimeExclusive = endTimeExclusive;
            this.Cost = cost;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetAllSubscribersDbStatement 
    {
        public GetAllSubscribersDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetCreatorsAndFirstSubscribedDatesDbStatement 
    {
        public GetCreatorsAndFirstSubscribedDatesDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class ProcessPaymentProcessingData 
    {
        public ProcessPaymentProcessingData(
            Fifthweek.Payments.Services.ISubscriberPaymentPipeline subscriberPaymentPipeline)
        {
            if (subscriberPaymentPipeline == null)
            {
                throw new ArgumentNullException("subscriberPaymentPipeline");
            }

            this.subscriberPaymentPipeline = subscriberPaymentPipeline;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetPaymentProcessingData
    {
        public partial class CachedData 
        {
            public CachedData(
                Fifthweek.Api.Identity.Shared.Membership.UserId subscriberId,
                Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
                System.DateTime startTimeInclusive,
                System.DateTime endTimeExclusive,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberChannelsSnapshot> subscriberChannelsSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberSnapshot> subscriberSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CalculatedAccountBalanceSnapshot> calculatedAccountBalanceSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorChannelsSnapshot> creatorChannelsSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorFreeAccessUsersSnapshot> creatorFreeAccessUsersSnapshots,
                System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Services.CreatorPost> creatorPosts,
                Fifthweek.Payments.Services.CreatorPercentageOverrideData creatorPercentageOverride)
            {
                if (subscriberId == null)
                {
                    throw new ArgumentNullException("subscriberId");
                }

                if (creatorId == null)
                {
                    throw new ArgumentNullException("creatorId");
                }

                if (startTimeInclusive == null)
                {
                    throw new ArgumentNullException("startTimeInclusive");
                }

                if (endTimeExclusive == null)
                {
                    throw new ArgumentNullException("endTimeExclusive");
                }

                if (subscriberChannelsSnapshots == null)
                {
                    throw new ArgumentNullException("subscriberChannelsSnapshots");
                }

                if (subscriberSnapshots == null)
                {
                    throw new ArgumentNullException("subscriberSnapshots");
                }

                if (calculatedAccountBalanceSnapshots == null)
                {
                    throw new ArgumentNullException("calculatedAccountBalanceSnapshots");
                }

                if (creatorChannelsSnapshots == null)
                {
                    throw new ArgumentNullException("creatorChannelsSnapshots");
                }

                if (creatorFreeAccessUsersSnapshots == null)
                {
                    throw new ArgumentNullException("creatorFreeAccessUsersSnapshots");
                }

                if (creatorPosts == null)
                {
                    throw new ArgumentNullException("creatorPosts");
                }

                this.SubscriberId = subscriberId;
                this.CreatorId = creatorId;
                this.StartTimeInclusive = startTimeInclusive;
                this.EndTimeExclusive = endTimeExclusive;
                this.SubscriberChannelsSnapshots = subscriberChannelsSnapshots;
                this.SubscriberSnapshots = subscriberSnapshots;
                this.CalculatedAccountBalanceSnapshots = calculatedAccountBalanceSnapshots;
                this.CreatorChannelsSnapshots = creatorChannelsSnapshots;
                this.CreatorFreeAccessUsersSnapshots = creatorFreeAccessUsersSnapshots;
                this.CreatorPosts = creatorPosts;
                this.CreatorPercentageOverride = creatorPercentageOverride;
            }
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetPaymentProcessingData 
    {
        public GetPaymentProcessingData(
            Fifthweek.Payments.Services.IGetCreatorChannelsSnapshotsDbStatement getCreatorChannelsSnapshots,
            Fifthweek.Payments.Services.IGetCreatorFreeAccessUsersSnapshotsDbStatement getCreatorFreeAccessUsersSnapshots,
            Fifthweek.Payments.Services.IGetCreatorPostsDbStatement getCreatorPosts,
            Fifthweek.Payments.Services.IGetSubscriberChannelsSnapshotsDbStatement getSubscriberChannelsSnapshots,
            Fifthweek.Payments.Services.IGetSubscriberSnapshotsDbStatement getSubscriberSnapshots,
            Fifthweek.Payments.Services.IGetCalculatedAccountBalancesDbStatement getCalculatedAccountBalances,
            Fifthweek.Payments.Services.IGetCreatorPercentageOverrideDbStatement getCreatorPercentageOverride)
        {
            if (getCreatorChannelsSnapshots == null)
            {
                throw new ArgumentNullException("getCreatorChannelsSnapshots");
            }

            if (getCreatorFreeAccessUsersSnapshots == null)
            {
                throw new ArgumentNullException("getCreatorFreeAccessUsersSnapshots");
            }

            if (getCreatorPosts == null)
            {
                throw new ArgumentNullException("getCreatorPosts");
            }

            if (getSubscriberChannelsSnapshots == null)
            {
                throw new ArgumentNullException("getSubscriberChannelsSnapshots");
            }

            if (getSubscriberSnapshots == null)
            {
                throw new ArgumentNullException("getSubscriberSnapshots");
            }

            if (getCalculatedAccountBalances == null)
            {
                throw new ArgumentNullException("getCalculatedAccountBalances");
            }

            if (getCreatorPercentageOverride == null)
            {
                throw new ArgumentNullException("getCreatorPercentageOverride");
            }

            this.getCreatorChannelsSnapshots = getCreatorChannelsSnapshots;
            this.getCreatorFreeAccessUsersSnapshots = getCreatorFreeAccessUsersSnapshots;
            this.getCreatorPosts = getCreatorPosts;
            this.getSubscriberChannelsSnapshots = getSubscriberChannelsSnapshots;
            this.getSubscriberSnapshots = getSubscriberSnapshots;
            this.getCalculatedAccountBalances = getCalculatedAccountBalances;
            this.getCreatorPercentageOverride = getCreatorPercentageOverride;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Shared;

    public partial class ProcessPaymentsForSubscriber 
    {
        public ProcessPaymentsForSubscriber(
            Fifthweek.Payments.Services.IGetCreatorsAndFirstSubscribedDatesDbStatement getCreatorsAndFirstSubscribedDates,
            Fifthweek.Payments.Services.IProcessPaymentsBetweenSubscriberAndCreator processPaymentsBetweenSubscriberAndCreator,
            Fifthweek.Payments.Services.IGetLatestCommittedLedgerDateDbStatement getLatestCommittedLedgerDate)
        {
            if (getCreatorsAndFirstSubscribedDates == null)
            {
                throw new ArgumentNullException("getCreatorsAndFirstSubscribedDates");
            }

            if (processPaymentsBetweenSubscriberAndCreator == null)
            {
                throw new ArgumentNullException("processPaymentsBetweenSubscriberAndCreator");
            }

            if (getLatestCommittedLedgerDate == null)
            {
                throw new ArgumentNullException("getLatestCommittedLedgerDate");
            }

            this.getCreatorsAndFirstSubscribedDates = getCreatorsAndFirstSubscribedDates;
            this.processPaymentsBetweenSubscriberAndCreator = processPaymentsBetweenSubscriberAndCreator;
            this.getLatestCommittedLedgerDate = getLatestCommittedLedgerDate;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PaymentProcessingData 
    {
        public PaymentProcessingData(
            Fifthweek.Api.Identity.Shared.Membership.UserId subscriberId,
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.DateTime startTimeInclusive,
            System.DateTime endTimeExclusive,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberChannelsSnapshot> subscriberChannelsSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.SubscriberSnapshot> subscriberSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CalculatedAccountBalanceSnapshot> calculatedAccountBalanceSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorChannelsSnapshot> creatorChannelsSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Snapshots.CreatorFreeAccessUsersSnapshot> creatorFreeAccessUsersSnapshots,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Services.CreatorPost> creatorPosts,
            Fifthweek.Payments.Services.CreatorPercentageOverrideData creatorPercentageOverride)
        {
            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (startTimeInclusive == null)
            {
                throw new ArgumentNullException("startTimeInclusive");
            }

            if (endTimeExclusive == null)
            {
                throw new ArgumentNullException("endTimeExclusive");
            }

            if (subscriberChannelsSnapshots == null)
            {
                throw new ArgumentNullException("subscriberChannelsSnapshots");
            }

            if (subscriberSnapshots == null)
            {
                throw new ArgumentNullException("subscriberSnapshots");
            }

            if (calculatedAccountBalanceSnapshots == null)
            {
                throw new ArgumentNullException("calculatedAccountBalanceSnapshots");
            }

            if (creatorChannelsSnapshots == null)
            {
                throw new ArgumentNullException("creatorChannelsSnapshots");
            }

            if (creatorFreeAccessUsersSnapshots == null)
            {
                throw new ArgumentNullException("creatorFreeAccessUsersSnapshots");
            }

            if (creatorPosts == null)
            {
                throw new ArgumentNullException("creatorPosts");
            }

            this.SubscriberId = subscriberId;
            this.CreatorId = creatorId;
            this.StartTimeInclusive = startTimeInclusive;
            this.EndTimeExclusive = endTimeExclusive;
            this.SubscriberChannelsSnapshots = subscriberChannelsSnapshots;
            this.SubscriberSnapshots = subscriberSnapshots;
            this.CalculatedAccountBalanceSnapshots = calculatedAccountBalanceSnapshots;
            this.CreatorChannelsSnapshots = creatorChannelsSnapshots;
            this.CreatorFreeAccessUsersSnapshots = creatorFreeAccessUsersSnapshots;
            this.CreatorPosts = creatorPosts;
            this.CreatorPercentageOverride = creatorPercentageOverride;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class ProcessPaymentsBetweenSubscriberAndCreator 
    {
        public ProcessPaymentsBetweenSubscriberAndCreator(
            Fifthweek.Payments.Services.IGetPaymentProcessingData getPaymentProcessingData,
            Fifthweek.Payments.Services.IProcessPaymentProcessingData processPaymentProcessingData,
            Fifthweek.Payments.Services.IPersistPaymentProcessingResults persistPaymentProcessingResults)
        {
            if (getPaymentProcessingData == null)
            {
                throw new ArgumentNullException("getPaymentProcessingData");
            }

            if (processPaymentProcessingData == null)
            {
                throw new ArgumentNullException("processPaymentProcessingData");
            }

            if (persistPaymentProcessingResults == null)
            {
                throw new ArgumentNullException("persistPaymentProcessingResults");
            }

            this.getPaymentProcessingData = getPaymentProcessingData;
            this.processPaymentProcessingData = processPaymentProcessingData;
            this.persistPaymentProcessingResults = persistPaymentProcessingResults;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetLatestCommittedLedgerDateDbStatement 
    {
        public GetLatestCommittedLedgerDateDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PaymentProcessingResult 
    {
        public PaymentProcessingResult(
            System.DateTime startTimeInclusive,
            System.DateTime endTimeExclusive,
            Fifthweek.Payments.AggregateCostSummary subscriptionCost,
            Fifthweek.Payments.Services.CreatorPercentageOverrideData creatorPercentageOverride,
            System.Boolean isComitted)
        {
            if (startTimeInclusive == null)
            {
                throw new ArgumentNullException("startTimeInclusive");
            }

            if (endTimeExclusive == null)
            {
                throw new ArgumentNullException("endTimeExclusive");
            }

            if (subscriptionCost == null)
            {
                throw new ArgumentNullException("subscriptionCost");
            }

            if (isComitted == null)
            {
                throw new ArgumentNullException("isComitted");
            }

            this.StartTimeInclusive = startTimeInclusive;
            this.EndTimeExclusive = endTimeExclusive;
            this.SubscriptionCost = subscriptionCost;
            this.CreatorPercentageOverride = creatorPercentageOverride;
            this.IsComitted = isComitted;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PaymentProcessingResults 
    {
        public PaymentProcessingResults(
            System.Collections.Generic.IReadOnlyList<Fifthweek.Payments.Services.PaymentProcessingResult> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            this.Items = items;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PersistPaymentProcessingResults 
    {
        public PersistPaymentProcessingResults(
            Fifthweek.Shared.IGuidCreator guidCreator,
            Fifthweek.Payments.Services.IPersistPaymentProcessingDataStatement persistPaymentProcessingData,
            Fifthweek.Payments.Services.IPersistCommittedAndUncommittedRecordsDbStatement persistCommittedAndUncommittedRecords)
        {
            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            if (persistPaymentProcessingData == null)
            {
                throw new ArgumentNullException("persistPaymentProcessingData");
            }

            if (persistCommittedAndUncommittedRecords == null)
            {
                throw new ArgumentNullException("persistCommittedAndUncommittedRecords");
            }

            this.guidCreator = guidCreator;
            this.persistPaymentProcessingData = persistPaymentProcessingData;
            this.persistCommittedAndUncommittedRecords = persistCommittedAndUncommittedRecords;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PersistedPaymentProcessingData 
    {
        public PersistedPaymentProcessingData(
            System.Guid id,
            Fifthweek.Payments.Services.PaymentProcessingData input,
            Fifthweek.Payments.Services.PaymentProcessingResults output)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            this.Id = id;
            this.Input = input;
            this.Output = output;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CreatorIdAndFirstSubscribedDate 
    {
        public CreatorIdAndFirstSubscribedDate(
            Fifthweek.Api.Identity.Shared.Membership.UserId creatorId,
            System.DateTime firstSubscribedDate)
        {
            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (firstSubscribedDate == null)
            {
                throw new ArgumentNullException("firstSubscribedDate");
            }

            this.CreatorId = creatorId;
            this.FirstSubscribedDate = firstSubscribedDate;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PersistCommittedAndUncommittedRecordsDbStatement 
    {
        public PersistCommittedAndUncommittedRecordsDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PersistPaymentProcessingDataStatement 
    {
        public PersistPaymentProcessingDataStatement(
            Fifthweek.Azure.ICloudStorageAccount cloudStorageAccount)
        {
            if (cloudStorageAccount == null)
            {
                throw new ArgumentNullException("cloudStorageAccount");
            }

            this.cloudStorageAccount = cloudStorageAccount;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class UpdateAccountBalancesDbStatement 
    {
        public UpdateAccountBalancesDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CreatorPercentageOverrideData 
    {
        public CreatorPercentageOverrideData(
            System.Decimal percentage,
            System.Nullable<System.DateTime> expiryDate)
        {
            if (percentage == null)
            {
                throw new ArgumentNullException("percentage");
            }

            this.Percentage = percentage;
            this.ExpiryDate = expiryDate;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetCreatorPercentageOverrideDbStatement 
    {
        public GetCreatorPercentageOverrideDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class GetCalculatedAccountBalancesDbStatement 
    {
        public GetCalculatedAccountBalancesDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class CalculatedAccountBalanceSnapshot 
    {
        public CalculatedAccountBalanceSnapshot(
            System.DateTime timestamp,
            Fifthweek.Api.Identity.Shared.Membership.UserId userId,
            Fifthweek.Api.Persistence.Payments.LedgerAccountType accountType,
            System.Decimal amount)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (accountType == null)
            {
                throw new ArgumentNullException("accountType");
            }

            if (amount == null)
            {
                throw new ArgumentNullException("amount");
            }

            this.Timestamp = timestamp;
            this.UserId = userId;
            this.AccountType = accountType;
            this.Amount = amount;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;

    public partial class RequestProcessPaymentsService 
    {
        public RequestProcessPaymentsService(
            IQueueService queueService)
        {
            if (queueService == null)
            {
                throw new ArgumentNullException("queueService");
            }

            this.queueService = queueService;
        }
    }
}

namespace Fifthweek.Payments
{
    using System;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Fifthweek.Payments.Pipeline;

    public partial class AggregateCostSummary 
    {
        public override string ToString()
        {
            return string.Format("AggregateCostSummary({0})", this.Cost == null ? "null" : this.Cost.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((AggregateCostSummary)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Cost != null ? this.Cost.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(AggregateCostSummary other)
        {
            if (!object.Equals(this.Cost, other.Cost))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class CreatorChannelsSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CreatorChannelsSnapshot({0}, {1}, {2})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.CreatorChannels == null ? "null" : this.CreatorChannels.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreatorChannelsSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorChannels != null 
        			? this.CreatorChannels.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorChannelsSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (this.CreatorChannels != null && other.CreatorChannels != null)
            {
                if (!this.CreatorChannels.SequenceEqual(other.CreatorChannels))
                {
                    return false;    
                }
            }
            else if (this.CreatorChannels != null || other.CreatorChannels != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class CreatorChannelsSnapshotItem 
    {
        public override string ToString()
        {
            return string.Format("CreatorChannelsSnapshotItem({0}, {1})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Price == null ? "null" : this.Price.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreatorChannelsSnapshotItem)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Price != null ? this.Price.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorChannelsSnapshotItem other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Price, other.Price))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class CreatorFreeAccessUsersSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CreatorFreeAccessUsersSnapshot({0}, {1}, {2})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.FreeAccessUserEmails == null ? "null" : this.FreeAccessUserEmails.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreatorFreeAccessUsersSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FreeAccessUserEmails != null 
        			? this.FreeAccessUserEmails.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorFreeAccessUsersSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (this.FreeAccessUserEmails != null && other.FreeAccessUserEmails != null)
            {
                if (!this.FreeAccessUserEmails.SequenceEqual(other.FreeAccessUserEmails))
                {
                    return false;    
                }
            }
            else if (this.FreeAccessUserEmails != null || other.FreeAccessUserEmails != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class MergedSnapshot 
    {
        public override string ToString()
        {
            return string.Format("MergedSnapshot({0}, {1}, {2}, {3}, {4}, {5})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorChannels == null ? "null" : this.CreatorChannels.ToString(), this.CreatorFreeAccessUsers == null ? "null" : this.CreatorFreeAccessUsers.ToString(), this.SubscriberChannels == null ? "null" : this.SubscriberChannels.ToString(), this.Subscriber == null ? "null" : this.Subscriber.ToString(), this.CalculatedAccountBalance == null ? "null" : this.CalculatedAccountBalance.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((MergedSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorChannels != null ? this.CreatorChannels.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorFreeAccessUsers != null ? this.CreatorFreeAccessUsers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberChannels != null ? this.SubscriberChannels.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Subscriber != null ? this.Subscriber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CalculatedAccountBalance != null ? this.CalculatedAccountBalance.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(MergedSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorChannels, other.CreatorChannels))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorFreeAccessUsers, other.CreatorFreeAccessUsers))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberChannels, other.SubscriberChannels))
            {
                return false;
            }
        
            if (!object.Equals(this.Subscriber, other.Subscriber))
            {
                return false;
            }
        
            if (!object.Equals(this.CalculatedAccountBalance, other.CalculatedAccountBalance))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class SubscriberChannelsSnapshot 
    {
        public override string ToString()
        {
            return string.Format("SubscriberChannelsSnapshot({0}, {1}, {2})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.SubscriberId == null ? "null" : this.SubscriberId.ToString(), this.SubscribedChannels == null ? "null" : this.SubscribedChannels.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((SubscriberChannelsSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscribedChannels != null 
        			? this.SubscribedChannels.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberChannelsSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            if (this.SubscribedChannels != null && other.SubscribedChannels != null)
            {
                if (!this.SubscribedChannels.SequenceEqual(other.SubscribedChannels))
                {
                    return false;    
                }
            }
            else if (this.SubscribedChannels != null || other.SubscribedChannels != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class SubscriberChannelsSnapshotItem 
    {
        public override string ToString()
        {
            return string.Format("SubscriberChannelsSnapshotItem({0}, {1}, {2})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.AcceptedPrice == null ? "null" : this.AcceptedPrice.ToString(), this.SubscriptionStartDate == null ? "null" : this.SubscriptionStartDate.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((SubscriberChannelsSnapshotItem)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPrice != null ? this.AcceptedPrice.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionStartDate != null ? this.SubscriptionStartDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberChannelsSnapshotItem other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPrice, other.AcceptedPrice))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriptionStartDate, other.SubscriptionStartDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class SubscriberSnapshot 
    {
        public override string ToString()
        {
            return string.Format("SubscriberSnapshot({0}, {1}, \"{2}\")", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.SubscriberId == null ? "null" : this.SubscriberId.ToString(), this.Email == null ? "null" : this.Email.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((SubscriberSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CreatorPost 
    {
        public override string ToString()
        {
            return string.Format("CreatorPost({0}, {1})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreatorPost)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorPost other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.LiveDate, other.LiveDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Payments.Services;

    public partial class CostPeriod 
    {
        public override string ToString()
        {
            return string.Format("CostPeriod({0}, {1}, {2})", this.StartTimeInclusive == null ? "null" : this.StartTimeInclusive.ToString(), this.EndTimeExclusive == null ? "null" : this.EndTimeExclusive.ToString(), this.Cost == null ? "null" : this.Cost.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CostPeriod)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.StartTimeInclusive != null ? this.StartTimeInclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EndTimeExclusive != null ? this.EndTimeExclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Cost != null ? this.Cost.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CostPeriod other)
        {
            if (!object.Equals(this.StartTimeInclusive, other.StartTimeInclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.EndTimeExclusive, other.EndTimeExclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.Cost, other.Cost))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PaymentProcessingData 
    {
        public override string ToString()
        {
            return string.Format("PaymentProcessingData({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})", this.SubscriberId == null ? "null" : this.SubscriberId.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.StartTimeInclusive == null ? "null" : this.StartTimeInclusive.ToString(), this.EndTimeExclusive == null ? "null" : this.EndTimeExclusive.ToString(), this.SubscriberChannelsSnapshots == null ? "null" : this.SubscriberChannelsSnapshots.ToString(), this.SubscriberSnapshots == null ? "null" : this.SubscriberSnapshots.ToString(), this.CalculatedAccountBalanceSnapshots == null ? "null" : this.CalculatedAccountBalanceSnapshots.ToString(), this.CreatorChannelsSnapshots == null ? "null" : this.CreatorChannelsSnapshots.ToString(), this.CreatorFreeAccessUsersSnapshots == null ? "null" : this.CreatorFreeAccessUsersSnapshots.ToString(), this.CreatorPosts == null ? "null" : this.CreatorPosts.ToString(), this.CreatorPercentageOverride == null ? "null" : this.CreatorPercentageOverride.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((PaymentProcessingData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.StartTimeInclusive != null ? this.StartTimeInclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EndTimeExclusive != null ? this.EndTimeExclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberChannelsSnapshots != null 
        			? this.SubscriberChannelsSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberSnapshots != null 
        			? this.SubscriberSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CalculatedAccountBalanceSnapshots != null 
        			? this.CalculatedAccountBalanceSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CreatorChannelsSnapshots != null 
        			? this.CreatorChannelsSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CreatorFreeAccessUsersSnapshots != null 
        			? this.CreatorFreeAccessUsersSnapshots.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CreatorPosts != null 
        			? this.CreatorPosts.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                hashCode = (hashCode * 397) ^ (this.CreatorPercentageOverride != null ? this.CreatorPercentageOverride.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PaymentProcessingData other)
        {
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.StartTimeInclusive, other.StartTimeInclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.EndTimeExclusive, other.EndTimeExclusive))
            {
                return false;
            }
        
            if (this.SubscriberChannelsSnapshots != null && other.SubscriberChannelsSnapshots != null)
            {
                if (!this.SubscriberChannelsSnapshots.SequenceEqual(other.SubscriberChannelsSnapshots))
                {
                    return false;    
                }
            }
            else if (this.SubscriberChannelsSnapshots != null || other.SubscriberChannelsSnapshots != null)
            {
                return false;
            }
        
            if (this.SubscriberSnapshots != null && other.SubscriberSnapshots != null)
            {
                if (!this.SubscriberSnapshots.SequenceEqual(other.SubscriberSnapshots))
                {
                    return false;    
                }
            }
            else if (this.SubscriberSnapshots != null || other.SubscriberSnapshots != null)
            {
                return false;
            }
        
            if (this.CalculatedAccountBalanceSnapshots != null && other.CalculatedAccountBalanceSnapshots != null)
            {
                if (!this.CalculatedAccountBalanceSnapshots.SequenceEqual(other.CalculatedAccountBalanceSnapshots))
                {
                    return false;    
                }
            }
            else if (this.CalculatedAccountBalanceSnapshots != null || other.CalculatedAccountBalanceSnapshots != null)
            {
                return false;
            }
        
            if (this.CreatorChannelsSnapshots != null && other.CreatorChannelsSnapshots != null)
            {
                if (!this.CreatorChannelsSnapshots.SequenceEqual(other.CreatorChannelsSnapshots))
                {
                    return false;    
                }
            }
            else if (this.CreatorChannelsSnapshots != null || other.CreatorChannelsSnapshots != null)
            {
                return false;
            }
        
            if (this.CreatorFreeAccessUsersSnapshots != null && other.CreatorFreeAccessUsersSnapshots != null)
            {
                if (!this.CreatorFreeAccessUsersSnapshots.SequenceEqual(other.CreatorFreeAccessUsersSnapshots))
                {
                    return false;    
                }
            }
            else if (this.CreatorFreeAccessUsersSnapshots != null || other.CreatorFreeAccessUsersSnapshots != null)
            {
                return false;
            }
        
            if (this.CreatorPosts != null && other.CreatorPosts != null)
            {
                if (!this.CreatorPosts.SequenceEqual(other.CreatorPosts))
                {
                    return false;    
                }
            }
            else if (this.CreatorPosts != null || other.CreatorPosts != null)
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorPercentageOverride, other.CreatorPercentageOverride))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PaymentProcessingResult 
    {
        public override string ToString()
        {
            return string.Format("PaymentProcessingResult({0}, {1}, {2}, {3}, {4})", this.StartTimeInclusive == null ? "null" : this.StartTimeInclusive.ToString(), this.EndTimeExclusive == null ? "null" : this.EndTimeExclusive.ToString(), this.SubscriptionCost == null ? "null" : this.SubscriptionCost.ToString(), this.CreatorPercentageOverride == null ? "null" : this.CreatorPercentageOverride.ToString(), this.IsComitted == null ? "null" : this.IsComitted.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((PaymentProcessingResult)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.StartTimeInclusive != null ? this.StartTimeInclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EndTimeExclusive != null ? this.EndTimeExclusive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionCost != null ? this.SubscriptionCost.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorPercentageOverride != null ? this.CreatorPercentageOverride.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsComitted != null ? this.IsComitted.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PaymentProcessingResult other)
        {
            if (!object.Equals(this.StartTimeInclusive, other.StartTimeInclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.EndTimeExclusive, other.EndTimeExclusive))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriptionCost, other.SubscriptionCost))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorPercentageOverride, other.CreatorPercentageOverride))
            {
                return false;
            }
        
            if (!object.Equals(this.IsComitted, other.IsComitted))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PaymentProcessingResults 
    {
        public override string ToString()
        {
            return string.Format("PaymentProcessingResults({0})", this.Items == null ? "null" : this.Items.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((PaymentProcessingResults)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Items != null 
        			? this.Items.Aggregate(0, (previous, current) => 
        				{ 
        				    unchecked
        				    {
        				        return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
        				    }
        				})
        			: 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PaymentProcessingResults other)
        {
            if (this.Items != null && other.Items != null)
            {
                if (!this.Items.SequenceEqual(other.Items))
                {
                    return false;    
                }
            }
            else if (this.Items != null || other.Items != null)
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class PersistedPaymentProcessingData 
    {
        public override string ToString()
        {
            return string.Format("PersistedPaymentProcessingData({0}, {1}, {2})", this.Id == null ? "null" : this.Id.ToString(), this.Input == null ? "null" : this.Input.ToString(), this.Output == null ? "null" : this.Output.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((PersistedPaymentProcessingData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Input != null ? this.Input.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Output != null ? this.Output.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(PersistedPaymentProcessingData other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.Input, other.Input))
            {
                return false;
            }
        
            if (!object.Equals(this.Output, other.Output))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CreatorIdAndFirstSubscribedDate 
    {
        public override string ToString()
        {
            return string.Format("CreatorIdAndFirstSubscribedDate({0}, {1})", this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.FirstSubscribedDate == null ? "null" : this.FirstSubscribedDate.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreatorIdAndFirstSubscribedDate)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FirstSubscribedDate != null ? this.FirstSubscribedDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorIdAndFirstSubscribedDate other)
        {
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.FirstSubscribedDate, other.FirstSubscribedDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Pipeline;
    using System.Collections.Generic;
    using Dapper;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using System.Threading;
    using Fifthweek.Azure;
    using Newtonsoft.Json;
    using Fifthweek.Api.Persistence.Identity;

    public partial class CreatorPercentageOverrideData 
    {
        public override string ToString()
        {
            return string.Format("CreatorPercentageOverrideData({0}, {1})", this.Percentage == null ? "null" : this.Percentage.ToString(), this.ExpiryDate == null ? "null" : this.ExpiryDate.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CreatorPercentageOverrideData)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Percentage != null ? this.Percentage.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ExpiryDate != null ? this.ExpiryDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorPercentageOverrideData other)
        {
            if (!object.Equals(this.Percentage, other.Percentage))
            {
                return false;
            }
        
            if (!object.Equals(this.ExpiryDate, other.ExpiryDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Payments.Snapshots
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;

    public partial class CalculatedAccountBalanceSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CalculatedAccountBalanceSnapshot({0}, {1}, {2}, {3})", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.AccountType == null ? "null" : this.AccountType.ToString(), this.Amount == null ? "null" : this.Amount.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((CalculatedAccountBalanceSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AccountType != null ? this.AccountType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Amount != null ? this.Amount.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CalculatedAccountBalanceSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.AccountType, other.AccountType))
            {
                return false;
            }
        
            if (!object.Equals(this.Amount, other.Amount))
            {
                return false;
            }
        
            return true;
        }
    }
}


