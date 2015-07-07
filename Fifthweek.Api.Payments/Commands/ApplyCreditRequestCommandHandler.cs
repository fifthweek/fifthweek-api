namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Taxamo;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Newtonsoft.Json;

    [AutoConstructor]
    [Decorator(OmitDefaultDecorators = true)]  // Turn off default retry policy so we don't repeatedly charge card if transient failure in Sql Azure.
    public partial class ApplyCreditRequestCommandHandler : ICommandHandler<ApplyCreditRequestCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IInitializeCreditRequest initializeCreditRequest;
        private readonly IPerformCreditRequest performCreditRequest;
        private readonly ICommitCreditToDatabase commitCreditToDatabase;
        private readonly IFifthweekRetryOnTransientErrorHandler retryOnTransientFailure;
        private readonly ICommitTaxamoTransaction commitTaxamoTransaction;

        private readonly ICommitTestUserCreditToDatabase commitTestUserCreditToDatabase;

        public async Task HandleAsync(ApplyCreditRequestCommand command)
        {
            command.AssertNotNull("command");
            await this.requesterSecurity.AuthenticateAsAsync(command.Requester, command.UserId);

            var isTestUser = await this.requesterSecurity.IsInRoleAsync(command.Requester, FifthweekRole.TestUser);
            if (isTestUser)
            {
                // For a test user we just update their account balance directly, 
                // as we don't want the credit to affect the Fifthweek accounts
                // or VAT related accounts.
                await this.retryOnTransientFailure.HandleAsync(() =>
                    this.commitTestUserCreditToDatabase.HandleAsync(
                        command.UserId,
                        command.Amount));

                return;
            }

            // We split this up into three phases that have individual retry handlers.
            // The first phase can be retried without issue if there are transient failures.
            var initializeResult = await this.retryOnTransientFailure.HandleAsync(() => 
                this.initializeCreditRequest.HandleAsync(command));

            // This phase could be put at the end of the first phase, but it runs the risk of someone inserting
            // a statement afterwards that causes a transient failure, so for safety it has been isolated.
            var stripeTransactionResult = await this.retryOnTransientFailure.HandleAsync(() =>
                this.performCreditRequest.HandleAsync(command, initializeResult.TaxamoTransaction, initializeResult.Origin));

            try
            {
                // Finally we commit to the local database...
                var commitToDatabaseTask = this.retryOnTransientFailure.HandleAsync(() =>
                    this.commitCreditToDatabase.HandleAsync(
                        command.UserId,
                        initializeResult.TaxamoTransaction,
                        initializeResult.Origin,
                        stripeTransactionResult));

                // ... and commit taxamo transaction.
                var commitToTaxamoTask = this.retryOnTransientFailure.HandleAsync(() =>
                    this.commitTaxamoTransaction.ExecuteAsync(initializeResult.TaxamoTransaction.Key));

                // We run the two committing tasks in parallel as even if one fails we would like the other to try and succeed.
                await Task.WhenAll(commitToDatabaseTask, commitToTaxamoTask);
            }
            catch (Exception t)
            {
                var json = JsonConvert.SerializeObject(
                    new 
                    {
                        UserId = command.UserId,
                        InitializeResult = initializeResult,
                        StripeTransactionResult = stripeTransactionResult,
                    });

                throw new FailedToApplyCreditException(json, t);
            }
        }
    }
}