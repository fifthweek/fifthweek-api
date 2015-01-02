namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using Fifthweek.Api.Identity.Membership.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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