namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetFeedbackUserDataDbStatement : IGetFeedbackUserDataDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT {2} AS Username, {3} FROM {0}
                WHERE {1}=@UserId",
            FifthweekUser.Table,
            FifthweekUser.Fields.Id,
            FifthweekUser.Fields.UserName,
            FifthweekUser.Fields.Email);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetFeedbackUserDataResult> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<GetFeedbackUserDataResult.Builder>(
                    Sql,
                    new
                    {
                        UserId = userId.Value
                    });

                var item = result.SingleOrDefault();
                if (item == null)
                {
                    throw new InvalidOperationException(
                        "UserId not found when requesting feedback user data: " + userId);
                }

                return item.Build();
            }
        }

        [AutoConstructor, AutoEqualityMembers, AutoCopy]
        public partial class GetFeedbackUserDataResult
        {
            public Email Email { get; private set; }

            public Username Username { get; private set; }
        }
    }
}