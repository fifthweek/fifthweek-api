namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Tests.Shared;

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
        private Mock<IFileSecurity> fileSecurity;
        private Mock<IPostFileTypeChecks> postFileTypeChecks;
        private Mock<IPostToCollectionDbStatement> postToCollectionDbStatement;
        private PostFileCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.collectionSecurity = new Mock<ICollectionSecurity>();
            this.fileSecurity = new Mock<IFileSecurity>();
            this.postFileTypeChecks = new Mock<IPostFileTypeChecks>();

            // Give side-effecting components strict mock behaviour.
            this.postToCollectionDbStatement = new Mock<IPostToCollectionDbStatement>(MockBehavior.Strict);

            this.target = new PostFileCommandHandler(
                this.collectionSecurity.Object,
                this.fileSecurity.Object,
                this.postFileTypeChecks.Object,
                this.postToCollectionDbStatement.Object);
        }

        [TestMethod]
        public async Task WhenNotAllowedToPost_ItShouldReportAnError()
        {
            this.collectionSecurity.Setup(_ => _.AssertPostingAllowedAsync(UserId, CollectionId)).Throws<UnauthorizedException>();

            Func<Task> badMethodCall = () => this.target.HandleAsync(Command);

            await badMethodCall.AssertExceptionAsync<UnauthorizedException>();
        }

        [TestMethod]
        public async Task WhenNotAllowedToUseFile_ItShouldReportAnError()
        {
            this.fileSecurity.Setup(_ => _.AssertUsageAllowedAsync(UserId, FileId)).Throws<UnauthorizedException>();

            Func<Task> badMethodCall = () => this.target.HandleAsync(Command);

            await badMethodCall.AssertExceptionAsync<UnauthorizedException>();
        }

        [TestMethod]
        public async Task WhenFileHasInvalidType_ItShouldReportAnError()
        {
            this.postFileTypeChecks.Setup(_ => _.AssertValidForFilePostAsync(FileId)).Throws(new RecoverableException("Bad file type"));

            Func<Task> badMethodCall = () => this.target.HandleAsync(Command);

            await badMethodCall.AssertExceptionAsync<RecoverableException>();
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