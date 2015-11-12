namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RevisePostCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ValidPreviewText PreviewText = ValidPreviewText.Parse("preview-text");
        private static readonly ValidComment Content = ValidComment.Parse("comment");
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly FileId ImageId = new FileId(Guid.NewGuid());
        private static readonly IReadOnlyList<FileId> FileIds = new List<FileId> { FileId, ImageId };
        private static readonly RevisePostCommand Command = new RevisePostCommand(Requester, PostId, ImageId, PreviewText, Content, 0, 1, 2, 3, 4, FileIds);
        private Mock<IPostSecurity> postSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IScheduleGarbageCollectionStatement> scheduleGarbageCollection;
        private Mock<IFileSecurity> fileSecurity;
        private Mock<IRevisePostDbStatement> revisePostDbStatement;
        private RevisePostCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.postSecurity = new Mock<IPostSecurity>();
            this.fileSecurity = new Mock<IFileSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            // Give potentially side-effective components strict mock behaviour.
            this.revisePostDbStatement = new Mock<IRevisePostDbStatement>(MockBehavior.Strict);
            this.scheduleGarbageCollection = new Mock<IScheduleGarbageCollectionStatement>(MockBehavior.Strict);

            this.target = new RevisePostCommandHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.scheduleGarbageCollection.Object,
                this.fileSecurity.Object,
                this.revisePostDbStatement.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new RevisePostCommand(Requester.Unauthenticated, PostId, ImageId, PreviewText, Content, 0, 1, 2, 3, 4, FileIds));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToWriteToPost_ItShouldThrowUnauthorizedException()
        {
            this.postSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, PostId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToUseFile_ItShouldThrowUnauthorizedException()
        {
            this.fileSecurity.Setup(_ => _.AssertReferenceAllowedAsync(UserId, FileId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenFileIdsDoNotContainPreviewImageId_ItShouldThrowRecoverableException()
        {
            await this.target.HandleAsync(new RevisePostCommand(Requester, PostId, ImageId, PreviewText, Content, 0, 1, 2, 3, 4, new List<FileId> { FileId }));

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenNoContent_ItShouldThrowRecoverableException()
        {
            await this.target.HandleAsync(new RevisePostCommand(Requester, PostId, ImageId, null, Content, 0, 1, 2, 3, 4, new List<FileId> { }));

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task WhenPostExists_ItShouldUpdateAndScheduleGarbageCollection()
        {
            this.revisePostDbStatement.Setup(
                v => v.ExecuteAsync(PostId, Content, PreviewText, ImageId, FileIds, 0, 1, 2, 3, 4))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(Command);

            this.revisePostDbStatement.Verify();
            this.scheduleGarbageCollection.Verify();
        }

        [TestMethod]
        public async Task WhenPostExistsAndNoPreviewImage_ItShouldUpdateAndScheduleGarbageCollection()
        {
            this.revisePostDbStatement.Setup(
                v => v.ExecuteAsync(PostId, Content, PreviewText, null, FileIds, 0, 1, 2, 3, 4))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(new RevisePostCommand(Requester, PostId, null, PreviewText, Content, 0, 1, 2, 3, 4, FileIds));

            this.revisePostDbStatement.Verify();
            this.scheduleGarbageCollection.Verify();
        }
    }
}