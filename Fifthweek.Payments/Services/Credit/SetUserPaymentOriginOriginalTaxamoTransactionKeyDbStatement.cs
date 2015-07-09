namespace Fifthweek.Payments.Services.Credit
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement : ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(
            UserId userId,
            string originalTaxamoTransactionKey)
        {
            userId.AssertNotNull("userId");
            originalTaxamoTransactionKey.AssertNotNull("originalTaxamoTransactionKey");

            var origin = new UserPaymentOrigin(
                userId.Value,
                null,
                null,
                null,
                null,
                null,
                originalTaxamoTransactionKey,
                default(BillingStatus));

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpsertAsync(origin, UserPaymentOrigin.Fields.OriginalTaxamoTransactionKey);
            }
        }
    }
}