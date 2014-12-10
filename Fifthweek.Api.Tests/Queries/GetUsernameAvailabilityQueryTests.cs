using Fifthweek.Api.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Queries
{
    [TestClass]
    public class GetUsernameAvailabilityQueryTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var query1 = new GetUsernameAvailabilityQuery("Lawrence");
            var query2 = new GetUsernameAvailabilityQuery("Lawrence");

            Assert.AreEqual(query1, query2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsernames()
        {
            var query1 = new GetUsernameAvailabilityQuery("Lawrence");
            var query2 = new GetUsernameAvailabilityQuery("James");

            Assert.AreNotEqual(query1, query2);
        }
    }
}