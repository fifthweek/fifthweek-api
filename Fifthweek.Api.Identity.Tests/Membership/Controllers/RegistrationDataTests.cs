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
        public void ItShouldHaveNullCustomPrimitivesBeforeParseIsCalled()
        {
            var data = NewData();

            Assert.IsNull(data.EmailObject);
            Assert.IsNull(data.PasswordObject);
            Assert.IsNull(data.UsernameObject);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldSetObjectPropertiesOnSuccess()
        {
            var data = NewData();
            data.Parse();

            Assert.AreEqual(data.EmailObject, Email.Parse(data.Email));
            Assert.AreEqual(data.PasswordObject, Password.Parse(data.Password));
            Assert.AreEqual(data.UsernameObject, Username.Parse(data.Username));
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