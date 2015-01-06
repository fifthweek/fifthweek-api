using System;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Tests.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ConfirmPasswordResetCommandTests : EqualityTests<ConfirmPasswordResetCommand, PasswordResetConfirmationData>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUserId()
        {
            this.AssertDifference(_ => _.UserId = Guid.NewGuid());
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentToken()
        {
            this.AssertDifference(_ => _.Token = "different");
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentPassword()
        {
            this.AssertDifference(_ => _.NewPassword = "different");
        }

        protected override PasswordResetConfirmationData NewInstanceOfBuilderForObjectA()
        {
            return PasswordResetConfirmationDataTests.NewData();
        }

        protected override ConfirmPasswordResetCommand FromBuilder(PasswordResetConfirmationData builder)
        {
            return NewCommand(builder);
        }

        public static ConfirmPasswordResetCommand NewCommand(PasswordResetConfirmationData passwordResetConfirmation)
        {
            return new ConfirmPasswordResetCommand(
                UserId.Parse(passwordResetConfirmation.UserId),
                passwordResetConfirmation.Token,
                Password.Parse(passwordResetConfirmation.NewPassword));
        }
    }
}