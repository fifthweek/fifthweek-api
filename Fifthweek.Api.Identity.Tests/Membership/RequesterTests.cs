namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;
    using System.Linq;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [TestClass]
    public class RequesterTests : PrimitiveTests<Requester>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void WhenUsingUnauthenticatedInstance_ItShouldNotHaveAUserId()
        {
            Assert.IsNull(Requester.Unauthenticated.UserId);
        }

        [TestMethod]
        public void WhenUsingUnauthenticatedInstance_ItShouldNotHaveAnyRoles()
        {
            Assert.IsFalse(Requester.Unauthenticated.Roles.Any());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCreatingAuthenticatedInstance_ItShouldNotAllowNullUserId()
        {
            Requester.Authenticated(null);
        }

        [TestMethod]
        public void WhenUsingAuthenticatedInstance_ItShouldContainTheAuthenticatedUserId()
        {
            var userId = new UserId(Guid.NewGuid());
            var target = Requester.Authenticated(userId);
            Assert.AreEqual(userId, target.UserId);
        }

        [TestMethod]
        public void WhenUsingAuthenticatedInstance_ItShouldContainTheAuthenticatedRoles()
        {
            var userId = new UserId(Guid.NewGuid());
            var target = Requester.Authenticated(userId, "role1", "role2");
            Assert.AreEqual(2, target.Roles.Count());
            Assert.IsTrue(target.Roles.Contains("role1"));
            Assert.IsTrue(target.Roles.Contains("role2"));
            Assert.IsTrue(target.IsInRole("role1"));
            Assert.IsTrue(target.IsInRole("role2"));
            Assert.IsFalse(target.IsInRole("role3"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCallingIsInRole_ItShouldCheckRoleIsNotNull()
        {
            var userId = new UserId(Guid.NewGuid());
            var target = Requester.Authenticated(userId, "role1", "role2");
            target.IsInRole(null);
        }

        protected override Requester NewInstanceOfObjectA()
        {
            return Requester.Authenticated(new UserId(Guid.Parse("{316343AD-9E14-4292-9BA2-3E2803AD4497}")), "role1", "role2");
        }

        protected override Requester NewInstanceOfObjectB()
        {
            return Requester.Unauthenticated;
        }
    }
}