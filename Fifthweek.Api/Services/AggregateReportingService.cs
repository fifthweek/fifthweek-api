namespace Fifthweek.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Models;

    public class AggregateReportingService : IReportingService
    {
        private readonly IReportingService[] delegateServices;

        public AggregateReportingService(params IReportingService[] delegateServices)
        {
            this.delegateServices = delegateServices;
        }

        public async Task ReportErrorAsync(Exception exception, string identifier, Developer developer)
        {
            var errors = new List<Exception>(); 
            foreach (var s in this.delegateServices)
            {
                try
                {
                    await s.ReportErrorAsync(exception, identifier, developer);
                }
                catch (Exception t)
                {
                    errors.Add(t);
                }
            }

            if (errors.Count != 0)
            {
                throw new AggregateException("Failed to delegate errors to all reporting services.", errors);
            }
        }
    }
}