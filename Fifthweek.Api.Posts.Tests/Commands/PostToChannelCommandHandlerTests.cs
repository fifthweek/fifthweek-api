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
    public class PostToChannelCommandHandlerTests
    {
        private const bool IsQueued = false;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly FileId ImageId = new FileId(Guid.NewGuid());
        private static readonly IReadOnlyList<FileId> FileIds = new List<FileId> { FileId, ImageId };
        private static readonly DateTime Timestamp = DateTime.UtcNow;
        private static readonly DateTime? ScheduleDate = null;
        private static readonly ValidPreviewText PreviewText = ValidPreviewText.Parse("preview-text");
        private static readonly ValidComment Content = ValidComment.Parse("comment");
        private static readonly PostToChannelCommand Command = new PostToChannelCommand(Requester, PostId, ChannelId, ImageId, PreviewText, Content, 1, 2, 3, 4, FileIds, ScheduleDate, QueueId, Timestamp);
        private Mock<IQueueSecurity> queueSecurity;
        private Mock<IFileSecurity> fileSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostToChannelDbStatement> postToChannelDbStatement;
        private PostToChannelCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.queueSecurity = new Mock<IQueueSecurity>();
            this.fileSecurity = new Mock<IFileSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            // Give side-effecting components strict mock behaviour.
            this.postToChannelDbStatement = new Mock<IPostToChannelDbStatement>(MockBehavior.Strict);

            this.target = new PostToChannelCommandHandler(
                this.queueSecurity.Object,
                this.fileSecurity.Object,
                this.requesterSecurity.Object,
                this.postToChannelDbStatement.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new PostToChannelCommand(Requester.Unauthenticated, PostId, ChannelId, ImageId, PreviewText, Content, 1, 2, 3, 4, FileIds, ScheduleDate, QueueId, Timestamp));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToPost_ItShouldThrowUnauthorizedException()
        {
            this.queueSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, QueueId)).Throws<UnauthorizedException>();

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
            await this.target.HandleAsync(new PostToChannelCommand(Requester, PostId, ChannelId, ImageId, PreviewText, Content, 1, 2, 3, 4, new List<FileId> { FileId }, ScheduleDate, QueueId, Timestamp));

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenNoContent_ItShouldThrowRecoverableException()
        {
            await this.target.HandleAsync(new PostToChannelCommand(Requester, PostId, ChannelId, ImageId, null, Content, 1, 2, 3, 4, new List<FileId> { }, ScheduleDate, QueueId, Timestamp));

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task WhenAllowedToPost_ItShouldPostToCollection()
        {
            this.postToChannelDbStatement.Setup(
                _ => _.ExecuteAsync(PostId, ChannelId, Content, ScheduleDate, QueueId, PreviewText, ImageId, FileIds, 1, 2, 3, 4, Timestamp))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.postToChannelDbStatement.Verify();
        }

        [TestMethod]
        public async Task WhenAllowedToPostWithNoPreviewImage_ItShouldPostToCollection()
        {
            this.postToChannelDbStatement.Setup(
                _ => _.ExecuteAsync(PostId, ChannelId, Content, ScheduleDate, QueueId, PreviewText, null, FileIds, 1, 2, 3, 4, Timestamp))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new PostToChannelCommand(Requester, PostId, ChannelId, null, PreviewText, Content, 1, 2, 3, 4, FileIds, ScheduleDate, QueueId, Timestamp));

            this.postToChannelDbStatement.Verify();
        }
    }
}