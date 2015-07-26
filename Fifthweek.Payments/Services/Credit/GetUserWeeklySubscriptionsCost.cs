namespace Fifthweek.Payments.Services.Credit
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetUserWeeklySubscriptionsCost : IGetUserWeeklySubscriptionsCost
    {
        private static readonly string Sql = string.Format(
            "SELECT SUM({0}) FROM {1} WHERE {2}=@UserId",
            ChannelSubscription.Fields.AcceptedPrice,
            ChannelSubscription.Table,
            ChannelSubscription.Fields.UserId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<int> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(
                    Sql,
                    new
                        {
                            UserId = userId.Value,
                        });
            }
        }
    }
}