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
                    CreatorChannelsSnapshot.Default(now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(now, UserId.Random()),
                    SubscriberSnapshot.Default(now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.DefaultFifthweekCreditAccount(now, UserId.Random())),
            };

            Assert.AreSame(input, this.target.Execute(input));
        }
    }
}