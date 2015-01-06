using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Tests.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegisterUserCommandTests : EqualityTests<RegisterUserCommand, RegisterUserCommandTests.Builder>
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
        public void ItShouldRecogniseDifferentExampleWork()
        {
            this.AssertDifference(_ => _.Registration.ExampleWork = "different");
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentEmail()
        {
            this.AssertDifference(_ => _.Registration.Email = "different@different.com");
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsername()
        {
            this.AssertDifference(_ => _.Registration.Username = "different");
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentPassword()
        {
            this.AssertDifference(_ => _.Registration.Password = "different");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ItShouldRequireUserId()
        {
            // This approach is required when checking custom primitives that have non-nullable inner values (such as Guid, Int, etc).
            // Otherwise, the more terse `AssertRequired` method can be used.
            new RegisterUserCommand(
                userId: null,
                exampleWork: this.ObjectA.ExampleWork,
                email: this.ObjectA.Email,
                username: this.ObjectA.Username,
                password: this.ObjectA.Password);
        }

        [TestMethod]
        public void ItShouldNotRequireExampleWork()
        {
            this.AssertOptional(_ => _.Registration.ExampleWork = null);
        }

        [TestMethod]
        public void ItShouldRequireEmail()
        {
            this.AssertRequired(_ => _.Registration.Email = null);
        }

        [TestMethod]
        public void ItShouldRequireUsername()
        {
            this.AssertRequired(_ => _.Registration.Username = null);
        }

        [TestMethod]
        public void ItShouldRequirePassword()
        {
            this.AssertRequired(_ => _.Registration.Password = null);
        }

        protected override Builder NewInstanceOfBuilderForObjectA()
        {
            return new Builder
            {
                UserId = Guid.Parse("{6BE94E94-6280-414A-A189-41145C4223A2}"),
                Registration = RegistrationDataTests.NewData()
            };
        }

        protected override RegisterUserCommand FromBuilder(Builder builder)
        {
            return NewCommand(builder.UserId, builder.Registration);
        }

        public static RegisterUserCommand NewCommand(Guid userId, RegistrationData registrationData)
        {
            return new RegisterUserCommand(
                UserId.Parse(userId),
                registrationData.ExampleWork,
                registrationData.Email == null ? null : NormalizedEmail.Parse(registrationData.Email),
                registrationData.Username == null ? null : NormalizedUsername.Parse(registrationData.Username),
                registrationData.Password == null ? null : Password.Parse(registrationData.Password));
        }

        public class Builder
        {
            public Guid UserId { get; set; }

            public RegistrationData Registration { get; set; }
        }
    }
}