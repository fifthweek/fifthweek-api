namespace Fifthweek.Api.Availability.Queries
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    public class GetAvailabilityQueryHandler : IQueryHandler<GetAvailabilityQuery, AvailabilityResult>
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        private readonly IExceptionHandler exceptionHandler;

        public GetAvailabilityQueryHandler(IFifthweekDbContext fifthweekDbContext, IExceptionHandler exceptionHandler)
        {
            this.fifthweekDbContext = fifthweekDbContext;
            this.exceptionHandler = exceptionHandler;
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
                this.exceptionHandler.ReportExceptionAsync(t);
            }

            return new AvailabilityResult(true, database);
        }
    }
}