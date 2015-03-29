namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ScheduledDateClippingFunctionTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime Past = Now.AddDays(-1);
        private static readonly DateTime Future = Now.AddDays(1);
        private ScheduledDateClippingFunction target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new ScheduledDateClippingFunction();
        }

        [TestMethod]
        public async Task WhenDateIsProvidedAndIsInFuture_ItShouldScheduleForFuture()
        {
            var result = this.target.Apply(Now, Future);
            Assert.AreEqual(Future, result);
        }

        [TestMethod]
        public async Task WhenDateIsProvidedAndIsNow_ItShouldScheduleForNow()
        {
            var result = this.target.Apply(Now, Now);
            Assert.AreEqual(Now, result);
        }

        [TestMethod]
        public async Task WhenDateIsProvidedAndIsInPast_ItShouldScheduleForPast()
        {
            var result = this.target.Apply(Now, Past);
            Assert.AreEqual(Past, result);
        }

        [TestMethod]
        public async Task WhenDateIsProvidedInPastBeyondSqlDateTimeMin_ItShouldScheduleForSqlDateTimeMin()
        {
            var result = this.target.Apply(Now, SqlDateTime.MinValue.Value.AddTicks(-1));
            Assert.AreEqual(SqlDateTime.MinValue.Value, result);
        }

        [TestMethod]
        public async Task WhenDateIsProvidedAndIsInFutureBeyondSqlDateTimeMax_ItShouldScheduleForSqlDateTimeMax()
        {
            var result = this.target.Apply(Now, SqlDateTime.MaxValue.Value.AddTicks(1));
            Assert.AreEqual(SqlDateTime.MaxValue.Value, result);
        }

        [TestMethod]
        public async Task WhenDateIsNotProvided_ItShouldScheduleForNow()
        {
            var result = this.target.Apply(Now, null);
            Assert.AreEqual(Now, result);
        }
    }
}