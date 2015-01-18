namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RequesterTests : PrimitiveTests<Requester>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void WhenUsingUnauthenticatedInstance_ItShouldStateIsAuthenticatedFalse()
        {
            Assert.IsFalse(Requester.Unauthenticated.IsAuthenticated);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public void WhenUsingUnauthenticatedInstance_ItShouldThrowSecurityExceptionWhenAssertingAuthenticity()
        {
            Requester.Unauthenticated.AssertAuthenticated();
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public void WhenUsingUnauthenticatedInstance_ItShouldThrowSecurityExceptionWhenAssertingAuthenticity2()
        {
            UserId userId;
            Requester.Unauthenticated.AssertAuthenticated(out userId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenCreatingAuthenticatedInstance_ItShouldNotAllowNullUserId()
        {
            Requester.Authenticated(null);
        }

        [TestMethod]
        public void WhenUsingAuthenticatedInstance_ItShouldStateIsAuthenticatedTrue()
        {
            var userId = new UserId(Guid.NewGuid());
            var target = Requester.Authenticated(userId);
            Assert.IsTrue(target.IsAuthenticated);
        }

        [TestMethod]
        public void WhenUsingAuthenticatedInstance_ItShouldPassAssertion()
        {
            var userId = new UserId(Guid.NewGuid());
            var target = Requester.Authenticated(userId);
            target.AssertAuthenticated();
        }

        [TestMethod]
        public void WhenUsingAuthenticatedInstance_ItShouldPassAssertionAndReturnUserId()
        {
            var userId = new UserId(Guid.NewGuid());
            var target = Requester.Authenticated(userId);

            UserId actualUserId;
            target.AssertAuthenticated(out actualUserId);

            Assert.AreEqual(userId, actualUserId);
        }

        protected override Requester NewInstanceOfObjectA()
        {
            return Requester.Authenticated(new UserId(Guid.Parse("{316343AD-9E14-4292-9BA2-3E2803AD4497}")));
        }

        protected override Requester NewInstanceOfObjectB()
        {
            return Requester.Unauthenticated;
        }
    }
}