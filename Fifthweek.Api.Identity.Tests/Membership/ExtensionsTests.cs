namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void WhenAssertAuthenticatedCalled_ItShouldThrowAnExceptionIfNotAuthenticated()
        {
            Exception exception = null;
            try
            {
                ((UserId)null).AssertAuthenticated();
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
        }

        [TestMethod]
        public void WhenAssertAuthenticatedCalled_ItShouldContinueIfAuthenticated()
        {
            Exception exception = null;
            try
            {
                new UserId(Guid.NewGuid()).AssertAuthenticated();
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNull(exception);
        }

        [TestMethod]
        public void WhenAssertAuthorizedForCalled_ItShouldThrowAnExceptionIfNotAuthenticated()
        {
            Exception exception = null;
            try
            {
                ((UserId)null).AssertAuthorizedFor(new UserId(Guid.NewGuid()));
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
                new UserId(Guid.NewGuid()).AssertAuthorizedFor(new UserId(Guid.NewGuid()));
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
                new UserId(Guid.NewGuid()).AssertAuthorizedFor(null);
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
                var userId = new UserId(Guid.NewGuid());
                userId.AssertAuthorizedFor(userId);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNull(exception);
        }
    }
}