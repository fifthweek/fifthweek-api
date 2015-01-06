using System;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using Fifthweek.Api.Identity.Membership.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IsPasswordResetTokenValidQueryTests : ImmutableComplexTypeTests<IsPasswordResetTokenValidQuery, IsPasswordResetTokenValidQueryTests.Builder>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUserIds()
        {
            this.AssertDifference(_ => _.UserId = UserId.Parse(Guid.NewGuid()));
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentTokens()
        {
            this.AssertDifference(_ => _.Token = "different");
        }

        [TestMethod]
        public void ItShouldRequireUserId()
        {
            this.AssertRequired(_ => _.UserId = null);
        }

        [TestMethod]
        public void ItShouldRequireToken()
        {
            this.AssertRequired(_ => _.Token = null);
        }

        protected override Builder NewInstanceOfBuilderForObjectA()
        {
            return new Builder
            {
                UserId = UserId.Parse(Guid.Parse("{8712C764-315D-4393-A3EB-6CCA0DADAC68}")),
                Token = "token"
            };
        }

        protected override IsPasswordResetTokenValidQuery FromBuilder(Builder builder)
        {
            return new IsPasswordResetTokenValidQuery(builder.UserId, builder.Token);
        }

        public class Builder
        {
            public UserId UserId;
            public string Token;
        }
    }
}