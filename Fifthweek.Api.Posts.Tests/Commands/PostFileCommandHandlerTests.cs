namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Subscriptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostFileCommandHandlerTests
    {
        private const bool IsQueued = false;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly DateTime? ScheduleDate = null;
        private static readonly ValidComment Comment = ValidComment.Parse("Hey guys!");
        private static readonly PostFileCommand Command = new PostFileCommand(UserId, PostId, CollectionId, FileId, Comment, ScheduleDate, IsQueued);
        private Mock<ICollectionSecurity> collectionSecurity;
        private Mock<IPostToCollectionDbStatement> postToCollectionDbStatement;
        private PostFileCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.collectionSecurity = new Mock<ICollectionSecurity>();
            
            // Give side-effecting components strict mock behaviour.
            this.postToCollectionDbStatement = new Mock<IPostToCollectionDbStatement>(MockBehavior.Strict);

            this.target = new PostFileCommandHandler(this.collectionSecurity.Object, this.postToCollectionDbStatement.Object);
        }

        [TestMethod]
        public async Task WhenNotAllowedToPost_ItShouldReportAnError()
        {
            this.collectionSecurity.Setup(_ => _.AssertPostingAllowedAsync(UserId, CollectionId)).Throws<UnauthorizedException>();

            try
            {
                await this.target.HandleAsync(Command);
                Assert.Fail("Expected recoverable exception");
            }
            catch (UnauthorizedException)
            {
            }
        }

        [TestMethod]
        public async Task WhenAllowedToPost_ItShouldPostToCollection()
        {
            this.postToCollectionDbStatement.Setup(
                _ => _.ExecuteAsync(PostId, CollectionId, Comment, ScheduleDate, IsQueued, FileId, false, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.postToCollectionDbStatement.Verify();
        }
    }
}