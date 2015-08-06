namespace Fifthweek.WebJobs.GarbageCollection.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.GarbageCollection;
    using Fifthweek.Tests.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GarbageCollectionProcessorTests
    {
        private Mock<IRunGarbageCollection> runGarbageCollection;
        private Mock<IBlobLeaseFactory> blobLeaseFactory;

        private Mock<ILogger> logger;

        private GarbageCollectionProcessor target;

        [TestInitialize]
        public void Initialize()
        {
            this.runGarbageCollection = new Mock<IRunGarbageCollection>(MockBehavior.Strict);
            this.blobLeaseFactory = new Mock<IBlobLeaseFactory>(MockBehavior.Strict);
            
            this.logger = new Mock<ILogger>(MockBehavior.Strict);

            this.target = new GarbageCollectionProcessor(this.runGarbageCollection.Object, this.blobLeaseFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenMessageIsNull_ItShouldThrowAnException()
        {
            await this.target.RunGarbageCollectionAsync(null, this.logger.Object, CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenLoggerIsNull_ItShouldThrowAnException()
        {
            await this.target.RunGarbageCollectionAsync(new RunGarbageCollectionMessage(), null, CancellationToken.None);
        }

        [TestMethod]
        public async Task ItShouldRunGarbageCollection()
        {
            var cts = new CancellationTokenSource();

            var lease = new Mock<IBlobLease>(MockBehavior.Strict);
            this.blobLeaseFactory.Setup(v => v.Create(Shared.Constants.LeaseObjectName, cts.Token))
                .Returns(lease.Object);

            lease.Setup(v => v.TryAcquireLeaseAsync()).ReturnsAsync(true);

            this.runGarbageCollection.Setup(v => v.ExecuteAsync(this.logger.Object, lease.Object, cts.Token))
                .Returns(Task.FromResult(0)).Verifiable();

            lease.Setup(v => v.UpdateTimestampsAsync()).Returns(Task.FromResult(0)).Verifiable();
            lease.Setup(v => v.ReleaseLeaseAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.RunGarbageCollectionAsync(new RunGarbageCollectionMessage(), this.logger.Object, cts.Token);

            this.runGarbageCollection.Verify();
            lease.Verify();
        }

        [TestMethod]
        public async Task WhenLeaseNotAcquired_ItShouldNotRunGarbageCollection()
        {
            var cts = new CancellationTokenSource();

            var lease = new Mock<IBlobLease>(MockBehavior.Strict);
            this.blobLeaseFactory.Setup(v => v.Create(Shared.Constants.LeaseObjectName, cts.Token))
                .Returns(lease.Object);

            lease.Setup(v => v.TryAcquireLeaseAsync()).ReturnsAsync(false);

            await this.target.RunGarbageCollectionAsync(new RunGarbageCollectionMessage(), this.logger.Object, cts.Token);

            lease.Verify();
        }

        [TestMethod]
        public async Task WhenAnErrorOccurs_ItShouldLogAndRethrow()
        {
            var cts = new CancellationTokenSource();

            var lease = new Mock<IBlobLease>(MockBehavior.Strict);
            this.blobLeaseFactory.Setup(v => v.Create(Shared.Constants.LeaseObjectName, cts.Token))
                .Returns(lease.Object);

            lease.Setup(v => v.TryAcquireLeaseAsync()).Throws(new DivideByZeroException());

            this.logger.Setup(v => v.Error(It.IsAny<DivideByZeroException>())).Verifiable();

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.RunGarbageCollectionAsync(new RunGarbageCollectionMessage(), this.logger.Object, cts.Token));

            this.logger.Verify();
        }

        [TestMethod]
        public async Task WhenHandlingPoisonMessage_ItShouldLogWarning()
        {
            this.logger.Setup(v => v.Warn("Failed to run garbage collection.")).Verifiable();

            await this.target.HandlePoisonMessageAsync(null, this.logger.Object, CancellationToken.None);

            this.logger.Verify();
        }
    }
}