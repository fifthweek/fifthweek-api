namespace Fifthweek.Api.Posts.Tests.Queries
{
    using System;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorBacklogQueryHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFifthweekDbContext> databaseContext;

        private GetCreatorBacklogQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            
        }
    }
}