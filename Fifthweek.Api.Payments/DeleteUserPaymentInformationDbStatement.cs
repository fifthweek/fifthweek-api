namespace Fifthweek.Api.Payments
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteUserPaymentInformationDbStatement : IDeleteUserPaymentInformationDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            var origin = new UserPaymentOrigin(
                userId.Value,
                null,
                null,
                null,
                null,
                null,
                null,
                default(PaymentStatus));

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpsertAsync(origin, UserPaymentOrigin.Fields.StripeCustomerId);
            }
        }
    }
}