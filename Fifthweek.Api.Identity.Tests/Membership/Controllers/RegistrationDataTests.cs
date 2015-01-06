using System;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
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
            Assert.AreNotEqual(registration1, null);
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

        [TestMethod]
        public void ItShouldInitializeWithNullCustomPrimitives()
        {
            var data = NewData();

            Assert.IsNull(data.EmailObj);
            Assert.IsNull(data.PasswordObj);
            Assert.IsNull(data.UsernameObj);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldSetObjectPropertiesOnSuccess()
        {
            var data = NewData();
            data.Parse();

            Assert.AreEqual(data.EmailObj, Email.Parse(data.Email));
            Assert.AreEqual(data.PasswordObj, Password.Parse(data.Password));
            Assert.AreEqual(data.UsernameObj, Username.Parse(data.Username));
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfInvalid()
        {
            this.AssertInvalid(_ => _.Email = EmailTests.InvalidValue);
            this.AssertInvalid(_ => _.Password = PasswordTests.InvalidValue);
            this.AssertInvalid(_ => _.Username = UsernameTests.InvalidValue);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfAnyAreNull()
        {
            this.AssertInvalid(_ => _.Email = null);
            this.AssertInvalid(_ => _.Password = null);
            this.AssertInvalid(_ => _.Username = null);
        }

        private void AssertInvalid(Action<RegistrationData> setInvalidFieldValue)
        {
            try
            {
                var registration = NewData();
                setInvalidFieldValue(registration);
                registration.Parse();
                Assert.Fail("Expected model validation exception");
            }
            catch (ModelValidationException)
            {
            }
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