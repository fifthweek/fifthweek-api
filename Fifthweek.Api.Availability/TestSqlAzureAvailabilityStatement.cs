namespace Fifthweek.Api.Availability
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    [AutoConstructor]
    public partial class TestSqlAzureAvailabilityStatement : ITestSqlAzureAvailabilityStatement
    {
        private readonly IExceptionHandler exceptionHandler;

        private readonly ITransientErrorDetectionStrategy transientErrorDetectionStrategy;

        private readonly ICountUsersDbStatement countUsersDbStatement;

        public async Task<bool> ExecuteAsync()
        {
            bool database = false;
            try
            {
                // A simple test that the database is available.
                await this.countUsersDbStatement.ExecuteAsync();
                database = true;
            }
            catch (Exception t)
            {
                if (this.transientErrorDetectionStrategy.IsTransient(t))
                {
                    this.exceptionHandler.ReportExceptionAsync(
                        new TransientErrorException("A transient error occurred while checking SQL Azure availability.", t));
                }
                else
                {
                    this.exceptionHandler.ReportExceptionAsync(t);
                }
            }

            return database;
        }
    }
}