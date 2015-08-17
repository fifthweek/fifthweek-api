namespace Fifthweek.Api.Blogs.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetAllCreatorRevenuesQueryHandler : IQueryHandler<GetAllCreatorRevenuesQuery, GetAllCreatorRevenuesResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetAllCreatorRevenuesDbStatement getAllCreatorRevenuesDbStatement;
        private readonly ITimestampCreator timestampCreator;

        public async Task<GetAllCreatorRevenuesResult> HandleAsync(GetAllCreatorRevenuesQuery query)
        {
            query.AssertNotNull("command");

            await this.requesterSecurity.AuthenticateAsync(query.Requester);
            await this.requesterSecurity.AssertInRoleAsync(query.Requester, FifthweekRole.Administrator);

            var releasableRevenueDate = this.timestampCreator.ReleasableRevenueDate();

            return await this.getAllCreatorRevenuesDbStatement.ExecuteAsync(releasableRevenueDate);
        }
    }
}