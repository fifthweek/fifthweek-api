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

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ItShouldRequireUserId()
        {
            // This approach is required when checking custom primitives that have non-nullable inner values (such as Guid, Int, etc).
            // Otherwise, the more terse `AssertRequired` method can be used.
            new ConfirmPasswordResetCommand(
                userId: null, 
                token: this.ObjectA.Token, 
                newPassword: this.ObjectA.NewPassword);
        }

        [TestMethod]
        public void ItShouldRequireToken()
        {
            this.AssertRequired(_ => _.Token = null);
        }

        [TestMethod]
        public void ItShouldRequirePassword()
        {
            this.AssertRequired(_ => _.NewPassword = null);
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
                passwordResetConfirmation.NewPassword == null ? null : Password.Parse(passwordResetConfirmation.NewPassword));
        }
    }
}