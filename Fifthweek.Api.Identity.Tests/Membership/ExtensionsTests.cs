namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExtensionsTests
    {
        private static readonly UserId AuthenticatedUserId = new UserId(Guid.NewGuid());
        private static readonly Requester AuthenticatedRequester = Requester.Authenticated(AuthenticatedUserId);

        [TestMethod]
        public void WhenAssertAuthorizedForCalled_ItShouldThrowAnExceptionIfNotAuthenticated()
        {
            Exception exception = null;
            try
            {
                 Requester.Unauthenticated.AssertAuthorizedFor(new UserId(Guid.NewGuid()));
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
        }

        [TestMethod]
        public void WhenAssertAuthorizedForCalled_ItShouldThrowAnExceptionIfUserIdsDoNotMatch()
        {
            Exception exception = null;
            try
            {
                AuthenticatedRequester.AssertAuthorizedFor(new UserId(Guid.NewGuid()));
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
        }

        [TestMethod]
        public void WhenAssertAuthorizedForCalled_ItShouldContinueIfAuthenticatedAndAuthorizedForIsNull()
        {
            Exception exception = null;
            try
            {
                AuthenticatedRequester.AssertAuthorizedFor(null);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNull(exception);
        }

        [TestMethod]
        public void WhenAssertAuthorizedForCalled_ItShouldContinueAuthorized()
        {
            Exception exception = null;
            try
            {
                AuthenticatedRequester.AssertAuthorizedFor(AuthenticatedUserId);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNull(exception);
        }
    }
}