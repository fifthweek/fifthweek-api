namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Deletions.Shared;

    [AutoConstructor]
    public partial class ScheduleGarbageCollectionStatement : IScheduleGarbageCollectionStatement
    {
        private readonly IQueueService queueService;

        public Task ExecuteAsync()
        {
            return this.queueService.AddMessageToQueueAsync(
                WebJobs.Deletions.Shared.Constants.GarbageCollectionQueueName, 
                new RunGarbageCollectionMessage(), 
                null,
                WebJobs.Deletions.Shared.Constants.GarbageCollectionMessageInitialVisibilityDelay);
        }
    }
}