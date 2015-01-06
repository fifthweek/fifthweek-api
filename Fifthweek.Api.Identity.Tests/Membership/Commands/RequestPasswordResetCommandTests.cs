using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Tests.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RequestPasswordResetCommandTests : EqualityTests<RequestPasswordResetCommand, PasswordResetRequestData>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentEmail()
        {
            this.AssertDifference(_ => _.Email = "different@different.com");
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsername()
        {
            this.AssertDifference(_ => _.Username = "different");
        }

        protected override PasswordResetRequestData NewInstanceOfBuilderForObjectA()
        {
            return PasswordResetRequestDataTests.NewData();
        }

        protected override RequestPasswordResetCommand FromBuilder(PasswordResetRequestData builder)
        {
            return NewCommand(builder);
        }

        public static RequestPasswordResetCommand NewCommand(PasswordResetRequestData passwordResetRequest)
        {
            return new RequestPasswordResetCommand(
                passwordResetRequest.Email == null ? null : NormalizedEmail.Parse(passwordResetRequest.Email),
                passwordResetRequest.Username == null ? null : NormalizedUsername.Parse(passwordResetRequest.Username));
        }
    }
}