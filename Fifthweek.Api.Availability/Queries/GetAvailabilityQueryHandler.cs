namespace Fifthweek.Api.Availability.Queries
{
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    [AutoConstructor]
    public partial class GetAvailabilityQueryHandler : IQueryHandler<GetAvailabilityQuery, AvailabilityResult>
    {
        private readonly ITestSqlAzureAvailabilityStatement testSqlAzureAvailability;

        private readonly ITestPaymentsAvailabilityStatement testPaymentsAvailability;

        public async Task<AvailabilityResult> HandleAsync(GetAvailabilityQuery query)
        {
            query.AssertNotNull("query");

            var database = await this.testSqlAzureAvailability.ExecuteAsync();
            var payments = await this.testPaymentsAvailability.ExecuteAsync();

            return new AvailabilityResult(true, database, payments);
        }
    }
}