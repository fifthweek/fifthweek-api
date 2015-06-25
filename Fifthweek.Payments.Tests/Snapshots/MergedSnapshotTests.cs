namespace Fifthweek.Payments.Tests.Snapshots
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MergedSnapshotTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        [TestMethod]
        public void ItShouldSetTheTimeStampToTheMaximumOfItsComponents()
        {
            Assert.AreEqual(
                Now.AddDays(1),
                new MergedSnapshot(
                    CreatorChannelsSnapshot.Default(Now.AddDays(1), UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.Default(Now, UserId.Random(), default(LedgerAccountType))).Timestamp);

            Assert.AreEqual(
                Now.AddDays(1),
                new MergedSnapshot(
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now.AddDays(1), UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.Default(Now, UserId.Random(), default(LedgerAccountType))).Timestamp);

            Assert.AreEqual(
                Now.AddDays(1),
                new MergedSnapshot(
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now.AddDays(1), UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.Default(Now, UserId.Random(), default(LedgerAccountType))).Timestamp);

            Assert.AreEqual(
                Now.AddDays(1),
                new MergedSnapshot(
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now.AddDays(1), UserId.Random()),
                    CalculatedAccountBalanceSnapshot.Default(Now, UserId.Random(), default(LedgerAccountType))).Timestamp);

            Assert.AreEqual(
                Now.AddDays(1),
                new MergedSnapshot(
                    CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                    CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                    SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                    SubscriberSnapshot.Default(Now, UserId.Random()),
                    CalculatedAccountBalanceSnapshot.Default(Now.AddDays(1), UserId.Random(), default(LedgerAccountType))).Timestamp);
        }
    }
}