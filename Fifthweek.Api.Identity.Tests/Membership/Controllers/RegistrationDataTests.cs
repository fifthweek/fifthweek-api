using Email = Fifthweek.Api.Identity.Membership.Email;

namespace Fifthweek.Api.Identity.Tests.Membership.Controllers
{
    using Fifthweek.Api.Identity.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegistrationDataTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var registration1 = NewData();
            var registration2 = NewData();

            Assert.AreEqual(registration1, registration2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentExampleWork()
        {
            var registration1 = NewData();
            var registration2 = NewData();
            registration2.ExampleWork = "Different";

            Assert.AreNotEqual(registration1, registration2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentEmail()
        {
            var registration1 = NewData();
            var registration2 = NewData();
            registration2.Email = "different@example.com";

            Assert.AreNotEqual(registration1, registration2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsername()
        {
            var registration1 = NewData();
            var registration2 = NewData();
            registration2.Username = "Different";

            Assert.AreNotEqual(registration1, registration2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentPassword()
        {
            var registration1 = NewData();
            var registration2 = NewData();
            registration2.Password = "Different";

            Assert.AreNotEqual(registration1, registration2);
        }

        public static RegistrationData NewData()
        {
            return new RegistrationData
            {
                ExampleWork = "TestExampleWork",
                Email = "test@test.com",
                Username = "test_username",
                Password = "TestPassword"
            };
        }
    }
}