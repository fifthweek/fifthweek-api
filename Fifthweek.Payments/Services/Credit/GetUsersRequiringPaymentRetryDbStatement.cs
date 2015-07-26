namespace Fifthweek.Payments.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetUsersRequiringPaymentRetryDbStatement : IGetUsersRequiringPaymentRetryDbStatement
    {
        private static readonly string Sql = string.Format(
            "SELECT {1} FROM {0} WHERE {2}>{3} AND {2}<{4} AND {5} IS NOT NULL AND {6}<>{7}",
            UserPaymentOrigin.Table,
            UserPaymentOrigin.Fields.UserId,
            UserPaymentOrigin.Fields.PaymentStatus,
            (int)PaymentStatus.None,
            (int)PaymentStatus.Failed,
            UserPaymentOrigin.Fields.PaymentOriginKey,
            UserPaymentOrigin.Fields.PaymentOriginKeyType,
            (int)PaymentOriginKeyType.None);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<UserId>> ExecuteAsync()
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var results = await connection.QueryAsync<Guid>(Sql);

                return results.Select(v => new UserId(v)).ToList();
            }
        }
    }
}