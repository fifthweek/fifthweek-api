namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RequesterSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly Requester Requester = Requester.Authenticated(UserId, "role", "role2", "role3");

        private RequesterSecurity target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new RequesterSecurity();
        }

        // Authenticate
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAuthenticate_ItShouldThrowAnExceptionWhenRequesterIsNull()
        {
            await this.target.AuthenticateAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAuthenticate_ItShouldThrowAnExceptionWhenNotAuthenticated()
        {
            await this.target.AuthenticateAsync(Requester.Unauthenticated);
        }

        [TestMethod]
        public async Task WhenCallingAuthenticate_ItShouldReturnIfAuthenticated()
        {
            await this.target.AuthenticateAsync(Requester);
        }

        // AuthenticateAs
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAuthenticateAs_ItShouldThrowAnExceptionWhenRequesterIsNull()
        {
            await this.target.AuthenticateAsAsync(null, UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAuthenticateAs_ItShouldThrowAnExceptionWhenUserIdIsNull()
        {
            await this.target.AuthenticateAsAsync(Requester.Unauthenticated, null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAuthenticateAs_ItShouldThrowAnExceptionWhenNotAuthenticated()
        {
            await this.target.AuthenticateAsAsync(Requester.Unauthenticated, UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAuthenticateAs_ItShouldThrowAnExceptionWhenNotAuthenticatedAsTheSpecifiedUser()
        {
            await this.target.AuthenticateAsAsync(Requester, new UserId(Guid.NewGuid()));
        }

        [TestMethod]
        public async Task WhenCallingAuthenticateAs_ItShouldReturnWhenAuthenticatedAsTheSpecifiedUser()
        {
            await this.target.AuthenticateAsAsync(Requester, UserId);
        }

        // IsInRole
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInRole_ItShouldThrowAnExceptionWhenRequesterIsNull()
        {
            await this.target.IsInRoleAsync(null, "role");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInRole_ItShouldThrowAnExceptionWhenRoleIsNull()
        {
            await this.target.IsInRoleAsync(Requester, null);
        }

        [TestMethod]
        public async Task WhenCallingIsInRole_ItShouldReturnFalseWhenNotInRole()
        {
            Assert.IsFalse(await this.target.IsInRoleAsync(Requester, "bad"));
        }

        [TestMethod]
        public async Task WhenCallingIsInRole_ItShouldReturnTrueWhenInRole()
        {
            Assert.IsTrue(await this.target.IsInRoleAsync(Requester, "role2"));
        }

        // AssertInRole
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInRole_ItShouldThrowAnExceptionWhenRequesterIsNull()
        {
            await this.target.AssertInRoleAsync(null, "role");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInRole_ItShouldThrowAnExceptionWhenRoleIsNull()
        {
            await this.target.AssertInRoleAsync(Requester, null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAssertInRole_ItShouldThrowAnExceptionWhenNotInRole()
        {
            await this.target.AssertInRoleAsync(Requester, "bad");
        }

        [TestMethod]
        public async Task WhenCallingAssertInRole_ItShouldReturnWhenInRole()
        {
            await this.target.AssertInRoleAsync(Requester, "role2");
        }

        // IsInAnyRole
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInAnyRole_ItShouldThrowAnExceptionWhenRequesterIsNull()
        {
            await this.target.IsInAnyRoleAsync(null, "role");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInAnyRole_ItShouldThrowAnExceptionWhenRequesterIsNull2()
        {
            await this.target.IsInAnyRoleAsync(null, new List<string> { "role" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInAnyRole_ItShouldThrowAnExceptionWhenRolesIsNull()
        {
            await this.target.IsInAnyRoleAsync(Requester, (string[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInAnyRole_ItShouldThrowAnExceptionWhenRolesIsNull2()
        {
            await this.target.IsInAnyRoleAsync(Requester, (IEnumerable<string>)null);
        }

        [TestMethod]
        public async Task WhenCallingIsInAnyRole_ItShouldReturnFalseWhenNotInAnyRole()
        {
            Assert.IsFalse(await this.target.IsInAnyRoleAsync(Requester, "bad", "bad2"));
        }

        [TestMethod]
        public async Task WhenCallingIsInAnyRole_ItShouldReturnFalseWhenNotInAnyRole2()
        {
            Assert.IsFalse(await this.target.IsInAnyRoleAsync(Requester, new List<string> { "bad", "bad2" }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task WhenCallingIsInAnyRole_ItShouldThrowAnExceptionWhenNoRolesPassedIn()
        {
            await this.target.IsInAnyRoleAsync(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task WhenCallingIsInAnyRole_ItShouldThrowAnExceptionWhenNoRolesPassedIn2()
        {
            await this.target.IsInAnyRoleAsync(Requester, new List<string>());
        }

        [TestMethod]
        public async Task WhenCallingIsInAnyRole_ItShouldReturnTrueWhenInAnyRole()
        {
            Assert.IsTrue(await this.target.IsInAnyRoleAsync(Requester, "bad", "role2"));
        }

        [TestMethod]
        public async Task WhenCallingIsInAnyRole_ItShouldReturnTrueWhenInAnyRole2()
        {
            Assert.IsTrue(await this.target.IsInAnyRoleAsync(Requester, new List<string> { "bad", "role2" }));
        }

        [TestMethod]
        public async Task WhenCallingIsInAnyRole_ItShouldReturnTrueWhenInAllRoles()
        {
            Assert.IsTrue(await this.target.IsInAnyRoleAsync(Requester, "role", "role2"));
        }

        [TestMethod]
        public async Task WhenCallingIsInAnyRole_ItShouldReturnTrueWhenInAllRoles2()
        {
            Assert.IsTrue(await this.target.IsInAnyRoleAsync(Requester, new List<string> { "role", "role2" }));
        }

        // AssertInAnyRole
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInAnyRole_ItShouldThrowAnExceptionWhenRequesterIsNull()
        {
            await this.target.AssertInAnyRoleAsync(null, "role");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInAnyRole_ItShouldThrowAnExceptionWhenRequesterIsNull2()
        {
            await this.target.AssertInAnyRoleAsync(null, new List<string> { "role" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInAnyRole_ItShouldThrowAnExceptionWhenRolesIsNull()
        {
            await this.target.AssertInAnyRoleAsync(Requester, (string[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInAnyRole_ItShouldThrowAnExceptionWhenRolesIsNull2()
        {
            await this.target.AssertInAnyRoleAsync(Requester, (IEnumerable<string>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAssertInAnyRole_ItShouldReThrowAnExceptionWWhenNotInAnyRole()
        {
            await this.target.AssertInAnyRoleAsync(Requester, "bad", "bad2");
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAssertInAnyRole_ItShouldThrowAnExceptionWhenNotInAnyRole2()
        {
            await this.target.AssertInAnyRoleAsync(Requester, new List<string> { "bad", "bad2" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task WhenCallingAssertInAnyRole_ItShouldThrowAnExceptionWhenNoRolesPassedIn()
        {
            await this.target.AssertInAnyRoleAsync(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task WhenCallingAssertInAnyRole_ItShouldThrowAnExceptionWhenNoRolesPassedIn2()
        {
            await this.target.AssertInAnyRoleAsync(Requester, new List<string>());
        }

        [TestMethod]
        public async Task WhenCallingAssertInAnyRole_ItShouldReturnWhenInAnyRole()
        {
            await this.target.AssertInAnyRoleAsync(Requester, "bad", "role2");
        }

        [TestMethod]
        public async Task WhenCallingAssertInAnyRole_ItShouldReturnWhenInAnyRole2()
        {
            await this.target.AssertInAnyRoleAsync(Requester, new List<string> { "bad", "role2" });
        }

        [TestMethod]
        public async Task WhenCallingAssertInAnyRole_ItShouldReturnWhenInAllRoles()
        {
            await this.target.AssertInAnyRoleAsync(Requester, "role", "role2");
        }

        [TestMethod]
        public async Task WhenCallingAssertInAnyRole_ItShouldReturnWhenInAllRoles2()
        {
            await this.target.AssertInAnyRoleAsync(Requester, new List<string> { "role", "role2" });
        }

        // IsInAllRoles
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInAllRoles_ItShouldThrowAnExceptionWhenRequesterIsNull()
        {
            await this.target.IsInAllRolesAsync(null, "role");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInAllRoles_ItShouldThrowAnExceptionWhenRequesterIsNull2()
        {
            await this.target.IsInAllRolesAsync(null, new List<string> { "role" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInAllRoles_ItShouldThrowAnExceptionWhenRolesIsNull()
        {
            await this.target.IsInAllRolesAsync(Requester, (string[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingIsInAllRoles_ItShouldThrowAnExceptionWhenRolesIsNull2()
        {
            await this.target.IsInAllRolesAsync(Requester, (IEnumerable<string>)null);
        }

        [TestMethod]
        public async Task WhenCallingIsInAllRoles_ItShouldReturnFalseWhenNotInAnyRole()
        {
            Assert.IsFalse(await this.target.IsInAllRolesAsync(Requester, "bad", "bad2"));
        }

        [TestMethod]
        public async Task WhenCallingIsInAllRoles_ItShouldReturnFalseWhenNotInAnyRole2()
        {
            Assert.IsFalse(await this.target.IsInAllRolesAsync(Requester, new List<string> { "bad", "bad2" }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task WhenCallingIsInAllRoles_ItShouldThrowAnExceptionWhenNoRolesPassedIn()
        {
            await this.target.IsInAllRolesAsync(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task WhenCallingIsInAllRoles_ItShouldThrowAnExceptionWhenNoRolesPassedIn2()
        {
            await this.target.IsInAllRolesAsync(Requester, new List<string>());
        }

        [TestMethod]
        public async Task WhenCallingIsInAllRoles_ItShouldReturnFalseWhenInAnyRole()
        {
            Assert.IsFalse(await this.target.IsInAllRolesAsync(Requester, "bad", "role2"));
        }

        [TestMethod]
        public async Task WhenCallingIsInAllRoles_ItShouldReturnFalseWhenInAnyRole2()
        {
            Assert.IsFalse(await this.target.IsInAllRolesAsync(Requester, new List<string> { "bad", "role2" }));
        }

        [TestMethod]
        public async Task WhenCallingIsInAllRoles_ItShouldReturnTrueWhenInAllRoles()
        {
            Assert.IsTrue(await this.target.IsInAllRolesAsync(Requester, "role", "role2"));
        }

        [TestMethod]
        public async Task WhenCallingIsInAllRoles_ItShouldReturnTrueWhenInAllRoles2()
        {
            Assert.IsTrue(await this.target.IsInAllRolesAsync(Requester, new List<string> { "role", "role2" }));
        }

        // AssertInAllRoles
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldThrowAnExceptionWhenRequesterIsNull()
        {
            await this.target.AssertInAllRolesAsync(null, "role");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldThrowAnExceptionWhenRequesterIsNull2()
        {
            await this.target.AssertInAllRolesAsync(null, new List<string> { "role" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldThrowAnExceptionWhenRolesIsNull()
        {
            await this.target.AssertInAllRolesAsync(Requester, (string[])null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldThrowAnExceptionWhenRolesIsNull2()
        {
            await this.target.AssertInAllRolesAsync(Requester, (IEnumerable<string>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldReThrowAnExceptionWWhenNotInAnyRole()
        {
            await this.target.AssertInAllRolesAsync(Requester, "bad", "bad2");
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldThrowAnExceptionWhenNotInAnyRole2()
        {
            await this.target.AssertInAllRolesAsync(Requester, new List<string> { "bad", "bad2" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldThrowAnExceptionWhenNoRolesPassedIn()
        {
            await this.target.AssertInAllRolesAsync(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldThrowAnExceptionWhenNoRolesPassedIn2()
        {
            await this.target.AssertInAllRolesAsync(Requester, new List<string>());
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldThrowAnExceptionWhenInAnyRole()
        {
            await this.target.AssertInAllRolesAsync(Requester, "bad", "role2");
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCallingAssertInAllRoles_ItShouldThrowAnExceptionWhenInAnyRole2()
        {
            await this.target.AssertInAllRolesAsync(Requester, new List<string> { "bad", "role2" });
        }

        [TestMethod]
        public async Task WhenCallingAssertInAllRoles_ItShouldReturnWhenInAllRoles()
        {
            await this.target.AssertInAllRolesAsync(Requester, "role", "role2");
        }

        [TestMethod]
        public async Task WhenCallingAssertInAllRoles_ItShouldReturnWhenInAllRoles2()
        {
            await this.target.AssertInAllRolesAsync(Requester, new List<string> { "role", "role2" });
        }
    }
}