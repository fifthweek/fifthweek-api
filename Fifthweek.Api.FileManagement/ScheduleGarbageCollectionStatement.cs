namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.GarbageCollection.Shared;

    [AutoConstructor]
    public partial class ScheduleGarbageCollectionStatement : IScheduleGarbageCollectionStatement
    {
        private readonly IQueueService queueService;

        public Task ExecuteAsync()
        {
            return this.queueService.AddMessageToQueueAsync(
                WebJobs.GarbageCollection.Shared.Constants.GarbageCollectionQueueName, 
                new RunGarbageCollectionMessage(), 
                null,
                WebJobs.GarbageCollection.Shared.Constants.GarbageCollectionMessageInitialVisibilityDelay);
        }
    }
}