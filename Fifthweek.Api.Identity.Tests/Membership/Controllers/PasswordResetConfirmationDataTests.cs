using System;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership.Controllers
{
    using Fifthweek.Api.Identity.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordResetConfirmationDataTests : DataTransferObjectTests<PasswordResetConfirmationData>
    {
        [TestMethod]
        public void ItShouldHaveNullCustomPrimitivesBeforeParseIsCalled()
        {
            var data = NewData();

            Assert.IsNull(data.NewPasswordObj);
            Assert.IsNull(data.UserIdObj);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldSetObjectPropertiesOnSuccess()
        {
            var data = NewData();
            data.Parse();

            Assert.AreEqual(data.NewPasswordObj, Password.Parse(data.NewPassword));
            Assert.AreEqual(data.UserIdObj, UserId.Parse(data.UserId));
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfInvalid()
        {
            this.BadValue(_ => _.NewPassword = PasswordTests.InvalidValue);
        }

        [TestMethod]
        public void WhenParsingCustomPrimitives_ItShouldRaiseModelValidationExceptionIfPasswordNull()
        {
            this.BadValue(_ => _.NewPassword = null);
        }


        protected override PasswordResetConfirmationData NewInstanceOfObjectA()
        {
            return NewData();
        }

        protected override void Parse(PasswordResetConfirmationData obj)
        {
            obj.Parse();
        }

        public static PasswordResetConfirmationData NewData()
        {
            return new PasswordResetConfirmationData
            {
                UserId = Guid.Parse("{5E41A09B-0523-4FD3-BC82-9D5A02D2FB97}"),
                Token = "SomeLongBase64EncodedGumpf",
                NewPassword = "SecretSquirrel",
            };
        }
    }
}