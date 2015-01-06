using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;
using Email = Fifthweek.Api.Identity.Membership.Email;

namespace Fifthweek.Api.Identity.Tests.Membership.Controllers
{
    using Fifthweek.Api.Identity.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegistrationDataTests : DataTransferObjectTests<RegistrationData>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentExampleWork()
        {
            this.AssertDifference(_ => _.ExampleWork = "Different");
            this.AssertDifference(_ => _.ExampleWork = null);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentEmail()
        {
            this.AssertDifference(_ => _.Email = "Different");
            this.AssertDifference(_ => _.Email = null);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsername()
        {
            this.AssertDifference(_ => _.Username = "Different");
            this.AssertDifference(_ => _.Username = null);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentPassword()
        {
            this.AssertDifference(_ => _.Password = "Different");
            this.AssertDifference(_ => _.Password = null);
        }

        [TestMethod]
        public void ItShouldHaveNullCustomPrimitivesBeforeParseIsCalled()
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
            this.BadValue(_ => _.Email = EmailTests.InvalidValue);
            this.BadValue(_ => _.Password = PasswordTests.InvalidValue);
            this.BadValue(_ => _.Username = UsernameTests.InvalidValue);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfAnyAreNull()
        {
            this.BadValue(_ => _.Email = null);
            this.BadValue(_ => _.Password = null);
            this.BadValue(_ => _.Username = null);
        }

        protected override void Parse(RegistrationData obj)
        {
            obj.Parse();
        }

        protected override RegistrationData NewInstanceOfObjectA()
        {
            return NewData();
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