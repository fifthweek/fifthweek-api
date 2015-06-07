namespace Fifthweek.Payments.Tests.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class MockRequestSnapshotService : IRequestSnapshotService
    {
        private UserId userId;

        private SnapshotType snapshotType;

        private bool throwException;

        private TrackingConnectionFactory connectionFactory;

        public void ThrowException()
        {
            this.throwException = true;
        }

        public void VerifyConnectionDisposed(TrackingConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public void VerifyCalledWith(UserId userId, SnapshotType snapshotType)
        {
            Assert.AreEqual(userId, this.userId);
            Assert.AreEqual(snapshotType, this.snapshotType);
        }

        public Task ExecuteAsync(UserId userId, SnapshotType snapshotType)
        {
            this.userId = userId;
            this.snapshotType = snapshotType;

            if (this.throwException)
            {
                throw new SnapshotException();
            }

            if (this.connectionFactory != null)
            {
                Assert.IsTrue(this.connectionFactory.ConnectionDisposed);
            }

            return Task.FromResult(0);
        }
    }
}