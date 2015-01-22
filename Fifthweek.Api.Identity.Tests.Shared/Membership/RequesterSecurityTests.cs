namespace Fifthweek.Api.Identity.Tests.Shared.Membership
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    using Moq;

    public static class RequesterSecurityTests
    {
        public static void SetupFor(this Mock<IRequesterSecurity> requesterSecurity, Requester requester)
        {
            requesterSecurity.AssertNotNull("requesterSecurity");
            requester.AssertNotNull("requester");

            if (requester.Equals(Requester.Unauthenticated))
            {
                throw new ArgumentException("Requester must be authorized.", "requester");
            }

            requesterSecurity.Setup(v => v.AuthenticateAsync(Requester.Unauthenticated))
                .Throws(new UnauthorizedException());

            requesterSecurity.Setup(v => v.AuthenticateAsync(It.IsNotIn(Requester.Unauthenticated)))
                .Returns<Requester>(v => Task.FromResult(v.UserId));

            requesterSecurity.Setup(v => v.AuthenticateAsAsync(Requester.Unauthenticated, It.IsAny<UserId>()))
                .Throws(new UnauthorizedException());

            requesterSecurity.Setup(v => v.AuthenticateAsAsync(requester, It.IsNotIn(requester.UserId)))
                .Throws(new UnauthorizedException());

            requesterSecurity.Setup(v => v.AuthenticateAsAsync(requester, requester.UserId))
                .ReturnsAsync(requester.UserId);
        }
    }
}