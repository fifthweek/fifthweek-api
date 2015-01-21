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
        public void WhenAssertAuthenticatedAsCalled_ItShouldThrowAnExceptionIfNotAuthenticated()
        {
            Exception exception = null;
            try
            {
                 Requester.Unauthenticated.AssertAuthenticatedAs(new UserId(Guid.NewGuid()));
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
        }

        [TestMethod]
        public void WhenAssertAuthenticatedAsCalled_ItShouldThrowAnExceptionIfUserIdsDoNotMatch()
        {
            Exception exception = null;
            try
            {
                AuthenticatedRequester.AssertAuthenticatedAs(new UserId(Guid.NewGuid()));
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
        }

        [TestMethod]
        public void WhenAssertAuthenticatedAsCalled_ItShouldContinueIfAuthenticatedAndAuthorizedForIsNull()
        {
            Exception exception = null;
            try
            {
                AuthenticatedRequester.AssertAuthenticatedAs(null);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNull(exception);
        }

        [TestMethod]
        public void WhenAssertAuthenticatedAsCalled_ItShouldContinueAuthorized()
        {
            Exception exception = null;
            try
            {
                AuthenticatedRequester.AssertAuthenticatedAs(AuthenticatedUserId);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNull(exception);
        }
    }
}