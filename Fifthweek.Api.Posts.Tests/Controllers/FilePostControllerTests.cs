namespace Fifthweek.Api.Posts.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FilePostControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private Mock<ICommandHandler<PostToChannelCommand>> postFile;
        private Mock<ICommandHandler<RevisePostCommand>> reviseFile;
        private Mock<IRequesterContext> requesterContext;
        private Mock<IGuidCreator> guidCreator;
        private FilePostController target;

        [TestInitialize]
        public void Initialize()
        {
            this.postFile = new Mock<ICommandHandler<PostToChannelCommand>>();
            this.reviseFile = new Mock<ICommandHandler<RevisePostCommand>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new FilePostController(
                this.postFile.Object,
                this.reviseFile.Object,
                this.requesterContext.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPostingFile_ItShouldIssuePostFileCommand()
        {
            var data = new NewPostData(QueueId, FileId, null, null, true);
            var command = new PostToChannelCommand(Requester, PostId, QueueId, FileId, null, null, true);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(PostId.Value);
            this.postFile.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.PostFile(data);

            this.postFile.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPostingFile_WithoutSpecifyingNewFileData_ItShouldThrowBadRequestException()
        {
            await this.target.PostFile(null);
        }

        [TestMethod]
        public async Task WhenPuttingFile_ItShouldIssuePostFileCommand()
        {
            var data = new RevisedPostData(FileId, null);
            var command = new RevisePostCommand(Requester, PostId, FileId, null);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(PostId.Value);
            this.reviseFile.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.PutFile(PostId.Value.EncodeGuid(), data);

            this.postFile.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPuttingFile_WithoutSpecifyingRevisedFileId_ItShouldThrowBadRequestException()
        {
            await this.target.PutFile(string.Empty, new RevisedPostData(FileId, null));
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPuttingFile_WithoutSpecifyingRevisedFileData_ItShouldThrowBadRequestException()
        {
            await this.target.PutFile(PostId.Value.EncodeGuid(), null);
        }
    }
}