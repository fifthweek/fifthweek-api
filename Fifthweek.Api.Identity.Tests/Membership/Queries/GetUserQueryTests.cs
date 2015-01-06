using System;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using Fifthweek.Api.Identity.Membership.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetUserQueryTests : ImmutableComplexTypeTests<GetUserQuery, GetUserQueryTests.Builder>
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
        public void ItShouldRecogniseDifferentPasswords()
        {
            this.AssertDifference(_ => _.Password = Password.Parse("different"));
        }

        [TestMethod]
        public void ItShouldRequireUsername()
        {
            this.AssertRequired(_ => _.Username = null);
        }

        [TestMethod]
        public void ItShouldRequirePassword()
        {
            this.AssertRequired(_ => _.Password = null);
        }

        protected override Builder NewInstanceOfBuilderForObjectA()
        {
            return new Builder
            {
                Username = NormalizedUsername.Parse("joebloggs"),
                Password = Password.Parse("password")
            };
        }

        protected override GetUserQuery FromBuilder(Builder builder)
        {
            return new GetUserQuery(builder.Username, builder.Password);
        }

        public class Builder
        {
            public NormalizedUsername Username;
            public Password Password;
        }
    }
}