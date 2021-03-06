namespace Fifthweek.Payments.Services.Credit
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class IncrementPaymentStatusDbStatement : IIncrementPaymentStatusDbStatement
    {
        private static readonly string Sql = string.Format(
            "UPDATE {0} SET {2}={2}+1 WHERE {1} IN @UserIds AND {2}<{3} AND {4} IS NOT NULL AND {5}<>{6}",
            UserPaymentOrigin.Table,
            UserPaymentOrigin.Fields.UserId,
            UserPaymentOrigin.Fields.PaymentStatus,
            (int)PaymentStatus.Failed,
            UserPaymentOrigin.Fields.PaymentOriginKey,
            UserPaymentOrigin.Fields.PaymentOriginKeyType,
            (int)PaymentOriginKeyType.None);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(IReadOnlyList<UserId> userIds)
        {
            userIds.AssertNotNull("userIds");

            if (userIds.Count == 0)
            {
                return;
            }

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    Sql,
                    new
                        {
                            UserIds = userIds.Select(v => v.Value).ToList(),
                        });
            }
        }
    }
}