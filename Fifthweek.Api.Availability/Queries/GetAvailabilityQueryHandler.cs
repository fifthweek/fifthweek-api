namespace Fifthweek.Api.Availability.Queries
{
    using System;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    [AutoConstructor]
    public partial class GetAvailabilityQueryHandler : IQueryHandler<GetAvailabilityQuery, AvailabilityResult>
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        private readonly IExceptionHandler exceptionHandler;

        private readonly ITransientErrorDetectionStrategy transientErrorDetectionStrategy;

        public async Task<AvailabilityResult> HandleAsync(GetAvailabilityQuery query)
        {
            query.AssertNotNull("query");

            var database = await this.TestSqlAzureAvailability();

            return new AvailabilityResult(true, database);
        }

        private async Task<bool> TestSqlAzureAvailability()
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
                if (this.transientErrorDetectionStrategy.IsTransient(t))
                {
                    this.exceptionHandler.ReportExceptionAsync(
                        new TransientErrorException("A transient error occured while checking SQL Azure availability.", t));
                }
                else
                {
                    this.exceptionHandler.ReportExceptionAsync(t);
                }
            }

            return database;
        }

        public class TransientErrorException : Exception
        {
            public TransientErrorException(string message, Exception exception)
                : base(message, exception)
            {
            }
        }
    }
}