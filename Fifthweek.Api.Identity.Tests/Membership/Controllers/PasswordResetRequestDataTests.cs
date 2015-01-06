using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership.Controllers
{
    using Fifthweek.Api.Identity.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordResetRequestDataTests : DataTransferObjectTests<PasswordResetRequestData>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
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
        public void ItShouldHaveNullCustomPrimitivesBeforeParseIsCalled()
        {
            var data = NewData();

            Assert.IsNull(data.EmailObj);
            Assert.IsNull(data.UsernameObj);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldSetObjectPropertiesOnSuccess()
        {
            var data = NewData();
            data.Parse();

            Assert.AreEqual(data.EmailObj, Email.Parse(data.Email));
            Assert.AreEqual(data.UsernameObj, Username.Parse(data.Username));
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfInvalid()
        {
            this.BadValue(_ => _.Email = EmailTests.InvalidValue);
            this.BadValue(_ => _.Username = UsernameTests.InvalidValue);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldAllowNullOnAllFields()
        {
            this.GoodValue(_ => _.Email = null);
            this.GoodValue(_ => _.Username = null);
        }

        protected override PasswordResetRequestData NewInstanceOfObjectA()
        {
            return NewData();
        }
        
        protected override void Parse(PasswordResetRequestData obj)
        {
            obj.Parse();
        }

        public static PasswordResetRequestData NewData()
        {
            return new PasswordResetRequestData
            {
                Email = "test@test.com",
                Username = "test_username",
            };
        }
    }
}