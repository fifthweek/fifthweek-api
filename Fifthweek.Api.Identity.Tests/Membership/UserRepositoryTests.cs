namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UserRepositoryTests : PersistenceTestsBase
    {
        private Mock<IFifthweekDbContext> fifthweekDbContext;
        private UserRepository target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fifthweekDbContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);
            this.target = new UserRepository(this.fifthweekDbContext.Object);
        }
    }
}