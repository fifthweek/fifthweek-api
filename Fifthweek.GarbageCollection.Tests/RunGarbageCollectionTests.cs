namespace Fifthweek.GarbageCollection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RunGarbageCollectionTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime ExpectedEndTime = Now.Subtract(GarbageCollection.Shared.Constants.GarbageCollectionMinimumAge);

        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IDeleteTestUserAccountsDbStatement> deleteTestUserAccounts;
        private Mock<IGetFilesEligibleForGarbageCollectionDbStatement> getFilesEligibleForGarbageCollection;
        private Mock<IDeleteBlobsForFile> deleteBlobsForFile;
        private Mock<IDeleteFileDbStatement> deleteFileDbStatement;
        private Mock<IDeleteOrphanedBlobContainers> deleteOrphanedBlobContainers;

        private Mock<IKeepAliveHandler> keepAliveHandler;
        private Mock<ILogger> logger;

        private RunGarbageCollection target;

        [TestInitialize]
        public void Initialize()
        {
            this.timestampCreator = new Mock<ITimestampCreator>(MockBehavior.Strict);
            this.deleteTestUserAccounts = new Mock<IDeleteTestUserAccountsDbStatement>(MockBehavior.Strict);
            this.getFilesEligibleForGarbageCollection = new Mock<IGetFilesEligibleForGarbageCollectionDbStatement>(MockBehavior.Strict);
            this.deleteBlobsForFile = new Mock<IDeleteBlobsForFile>(MockBehavior.Strict);
            this.deleteFileDbStatement = new Mock<IDeleteFileDbStatement>(MockBehavior.Strict);
            this.deleteOrphanedBlobContainers = new Mock<IDeleteOrphanedBlobContainers>(MockBehavior.Strict);

            this.keepAliveHandler = new Mock<IKeepAliveHandler>(MockBehavior.Strict);
            this.logger = new Mock<ILogger>();

            this.target = new RunGarbageCollection(
                this.timestampCreator.Object,
                this.deleteTestUserAccounts.Object,
                this.getFilesEligibleForGarbageCollection.Object,
                this.deleteBlobsForFile.Object,
                this.deleteFileDbStatement.Object,
                this.deleteOrphanedBlobContainers.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenLoggerIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, this.keepAliveHandler.Object, CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenKeepAliveHandlerIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(this.logger.Object, null, CancellationToken.None);
        }

        [TestMethod]
        public async Task ItShouldCallServices()
        {
            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.deleteTestUserAccounts.Setup(v => v.ExecuteAsync(ExpectedEndTime)).Returns(Task.FromResult(0)).Verifiable();

            var files = new List<OrphanedFileData>
            {
                new OrphanedFileData(FileId.Random(), ChannelId.Random(), "p1"),
                new OrphanedFileData(FileId.Random(), ChannelId.Random(), "p2"),
            };
            this.getFilesEligibleForGarbageCollection.Setup(v => v.ExecuteAsync(ExpectedEndTime)).ReturnsAsync(files);

            this.deleteBlobsForFile.Setup(v => v.ExecuteAsync(files[0])).Returns(Task.FromResult(0)).Verifiable();
            this.deleteFileDbStatement.Setup(v => v.ExecuteAsync(files[0].FileId)).Returns(Task.FromResult(0)).Verifiable();

            this.deleteBlobsForFile.Setup(v => v.ExecuteAsync(files[1])).Returns(Task.FromResult(0)).Verifiable();
            this.deleteFileDbStatement.Setup(v => v.ExecuteAsync(files[1].FileId)).Returns(Task.FromResult(0)).Verifiable();

            this.deleteOrphanedBlobContainers.Setup(v => v.ExecuteAsync(this.logger.Object, this.keepAliveHandler.Object, ExpectedEndTime, CancellationToken.None))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.keepAliveHandler.Setup(v => v.KeepAliveAsync()).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(this.logger.Object, this.keepAliveHandler.Object, CancellationToken.None);

            this.deleteTestUserAccounts.Verify();
            this.deleteBlobsForFile.Verify();
            this.deleteFileDbStatement.Verify();
            this.deleteOrphanedBlobContainers.Verify();
            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Exactly(2));
        }

        [TestMethod]
        public async Task WhenFailsToProcessFile_ItShouldLogAndContinue()
        {
            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.deleteTestUserAccounts.Setup(v => v.ExecuteAsync(ExpectedEndTime)).Returns(Task.FromResult(0)).Verifiable();

            var files = new List<OrphanedFileData>
            {
                new OrphanedFileData(FileId.Random(), ChannelId.Random(), "p1"),
                new OrphanedFileData(FileId.Random(), ChannelId.Random(), "p2"),
            };
            this.getFilesEligibleForGarbageCollection.Setup(v => v.ExecuteAsync(ExpectedEndTime)).ReturnsAsync(files);

            this.deleteBlobsForFile.Setup(v => v.ExecuteAsync(files[0])).Throws(new DivideByZeroException());
            this.logger.Setup(v => v.Error(It.IsAny<DivideByZeroException>())).Verifiable();

            this.deleteBlobsForFile.Setup(v => v.ExecuteAsync(files[1])).Returns(Task.FromResult(0)).Verifiable();
            this.deleteFileDbStatement.Setup(v => v.ExecuteAsync(files[1].FileId)).Returns(Task.FromResult(0)).Verifiable();

            this.deleteOrphanedBlobContainers.Setup(v => v.ExecuteAsync(this.logger.Object, this.keepAliveHandler.Object, ExpectedEndTime, CancellationToken.None))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.keepAliveHandler.Setup(v => v.KeepAliveAsync()).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(this.logger.Object, this.keepAliveHandler.Object, CancellationToken.None);

            this.deleteTestUserAccounts.Verify();
            this.deleteBlobsForFile.Verify();
            this.logger.Verify();
            this.deleteFileDbStatement.Verify();
            this.deleteOrphanedBlobContainers.Verify();
            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Exactly(2));
        }

        [TestMethod]
        public async Task WhenCancelled_ItShouldStopProcessing()
        {
            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.deleteTestUserAccounts.Setup(v => v.ExecuteAsync(ExpectedEndTime)).Returns(Task.FromResult(0)).Verifiable();

            var files = new List<OrphanedFileData>
            {
                new OrphanedFileData(FileId.Random(), ChannelId.Random(), "p1"),
                new OrphanedFileData(FileId.Random(), ChannelId.Random(), "p2"),
            };
            this.getFilesEligibleForGarbageCollection.Setup(v => v.ExecuteAsync(ExpectedEndTime)).ReturnsAsync(files);

            var cts = new CancellationTokenSource();

            this.deleteBlobsForFile.Setup(v => v.ExecuteAsync(files[0])).Returns(Task.FromResult(0)).Callback<OrphanedFileData>(a => cts.Cancel());
            this.deleteFileDbStatement.Setup(v => v.ExecuteAsync(files[0].FileId)).Returns(Task.FromResult(0)).Verifiable();

            this.keepAliveHandler.Setup(v => v.KeepAliveAsync()).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(this.logger.Object, this.keepAliveHandler.Object, cts.Token);

            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Exactly(1));
        }
    }
}