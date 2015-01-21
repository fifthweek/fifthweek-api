namespace Fifthweek.Api.FileManagement.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.WebJobs.GarbageCollection.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ScheduleGarbageCollectionStatementTests
    {
        private ScheduleGarbageCollectionStatement target;

        private Mock<IQueueService> queueService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.queueService = new Mock<IQueueService>();
            this.target = new ScheduleGarbageCollectionStatement(this.queueService.Object);
        }

        [TestMethod]
        public async Task WhenSchedulingGarbageCollection_ItShouldPostToTheAzureQueue()
        {
            this.queueService.Setup(v => v.AddMessageToQueueAsync(
                Constants.GarbageCollectionQueueName, 
                It.IsAny<RunGarbageCollectionMessage>(),
                null,
                Constants.GarbageCollectionMessageInitialVisibilityDelay))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.ExecuteAsync();

            this.queueService.Verify();
        }
    }
}