namespace Fifthweek.Api.QueryHandlers
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Models;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.Repositories;

    public class GetAvailabilityQueryHandler : IQueryHandler<GetAvailabilityQuery, AvailabilityResult>
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public GetAvailabilityQueryHandler(IFifthweekDbContext fifthweekDbContext)
        {
            this.fifthweekDbContext = fifthweekDbContext;
        }

        public async Task<AvailabilityResult> HandleAsync(GetAvailabilityQuery query)
        {
            bool database = false;
            try
            {
                // A simple test that the database is available.
                await this.fifthweekDbContext.Users.CountAsync();
                database = true;
            }
            catch (Exception t)
            {
                ExceptionHandlerUtilities.ReportExceptionAsync(t);
            }

            return new AvailabilityResult(true, database);
        }
    }
}