namespace Fifthweek.Api.Posts.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetFreePostTimestampTests
    {
        [TestMethod]
        public void ItShouldReturnExpectedTimestamps()
        {
            var target = new GetFreePostTimestamp();

            Assert.AreEqual(
                new DateTime(2015, 11, 16, 0, 0, 0, DateTimeKind.Utc),
                target.Execute(new DateTime(2015, 11, 16, 0, 0, 0, DateTimeKind.Utc)));

            Assert.AreEqual(
                new DateTime(2015, 11, 16, 0, 0, 0, DateTimeKind.Utc),
                target.Execute(new DateTime(2015, 11, 17, 1, 2, 3, DateTimeKind.Utc)));

            Assert.AreEqual(
                new DateTime(2015, 11, 16, 0, 0, 0, DateTimeKind.Utc),
                target.Execute(new DateTime(2015, 11, 22, 23, 59, 59, DateTimeKind.Utc)));

            Assert.AreEqual(
                new DateTime(2015, 11, 23, 0, 0, 0, DateTimeKind.Utc),
                target.Execute(new DateTime(2015, 11, 23, 0, 0, 0, DateTimeKind.Utc)));
        }
    }
}