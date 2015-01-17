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
    public class PostImageCommandHandlerTests
    {
        private const bool IsQueued = false;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly DateTime? ScheduleDate = null;
        private static readonly ValidComment Comment = ValidComment.Parse("Hey guys!");
        private static readonly PostImageCommand Command = new PostImageCommand(UserId, PostId, CollectionId, FileId, Comment, ScheduleDate, IsQueued);
        private Mock<ICollectionSecurity> collectionSecurity;
        private Mock<IFileSecurity> fileSecurity;
        private Mock<IPostFileTypeChecks> postFileTypeChecks;
        private Mock<IPostToCollectionDbStatement> postToCollectionDbStatement;
        private PostImageCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.collectionSecurity = new Mock<ICollectionSecurity>();
            this.fileSecurity = new Mock<IFileSecurity>();
            this.postFileTypeChecks = new Mock<IPostFileTypeChecks>();
            
            // Give side-effecting components strict mock behaviour.
            this.postToCollectionDbStatement = new Mock<IPostToCollectionDbStatement>(MockBehavior.Strict);

            this.target = new PostImageCommandHandler(
                this.collectionSecurity.Object, 
                this.fileSecurity.Object, 
                this.postFileTypeChecks.Object, 
                this.postToCollectionDbStatement.Object);
        }

        [TestMethod]
        public async Task WhenNotAllowedToPost_ItShouldReportAnError()
        {
            this.collectionSecurity.Setup(_ => _.AssertPostingAllowedAsync(UserId, CollectionId)).Throws<UnauthorizedException>();

            try
            {
                await this.target.HandleAsync(Command);
                Assert.Fail("Expected security exception");
            }
            catch (UnauthorizedException)
            {
            }
        }

        [TestMethod]
        public async Task WhenNotAllowedToUseFile_ItShouldReportAnError()
        {
            this.fileSecurity.Setup(_ => _.AssertUsageAllowedAsync(UserId, FileId)).Throws<UnauthorizedException>();

            try
            {
                await this.target.HandleAsync(Command);
                Assert.Fail("Expected security exception");
            }
            catch (UnauthorizedException)
            {
            }
        }

        [TestMethod]
        public async Task WhenFileHasInvalidType_ItShouldReportAnError()
        {
            this.postFileTypeChecks.Setup(_ => _.AssertValidForImagePostAsync(FileId)).Throws(new RecoverableException("Bad file type"));

            try
            {
                await this.target.HandleAsync(Command);
                Assert.Fail("Expected recoverable exception");
            }
            catch (RecoverableException)
            {
            }
        }

        [TestMethod]
        public async Task WhenAllowedToPost_ItShouldPostToCollection()
        {
            this.postToCollectionDbStatement.Setup(
                _ => _.ExecuteAsync(PostId, CollectionId, Comment, ScheduleDate, IsQueued, FileId, true, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.postToCollectionDbStatement.Verify();
        }
    }
}