namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RollBackSubscriptionsExecutorTests
    {
        private RollBackSubscriptionsExecutor target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new RollBackSubscriptionsExecutor();
        }

        [TestMethod]
        public void ItShouldReturnTheGivenSnapshots()
        {
            var now = DateTime.UtcNow;
            var input = new List<MergedSnapshot> 
            {
                new MergedSnapshot(
                    CreatorChannelsSnapshot.Default(now, new UserId(Guid.NewGuid())),
                    CreatorFreeAccessUsersSnapshot.Default(now, new UserId(Guid.NewGuid())),
                    SubscriberChannelsSnapshot.Default(now, new UserId(Guid.NewGuid())),
                    SubscriberSnapshot.Default(now, new UserId(Guid.NewGuid()))),
            };

            Assert.AreSame(input, this.target.Execute(input));
        }
    }
}