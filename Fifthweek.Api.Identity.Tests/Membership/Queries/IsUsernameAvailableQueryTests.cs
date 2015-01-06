using System;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using Fifthweek.Api.Identity.Membership.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IsUsernameAvailableQueryTests : ImmutableComplexTypeTests<IsUsernameAvailableQuery, IsUsernameAvailableQueryTests.Builder>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsernames()
        {
            this.AssertDifference(_ => _.Username = NormalizedUsername.Parse("different"));
        }

        [TestMethod]
        public void ItShouldRequireUsername()
        {
            this.AssertRequired(_ => _.Username = null);
        }

        public class Builder
        {
            public NormalizedUsername Username;
        }

        protected override Builder NewInstanceOfBuilderForObjectA()
        {
            return new Builder
            {
                Username = NormalizedUsername.Parse("joebloggs")
            };
        }

        protected override IsUsernameAvailableQuery FromBuilder(Builder builder)
        {
            return new IsUsernameAvailableQuery(builder.Username);
        }
    }
}