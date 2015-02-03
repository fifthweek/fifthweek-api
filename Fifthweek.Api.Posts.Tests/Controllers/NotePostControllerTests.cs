namespace Fifthweek.Api.Posts.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class NotePostControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidNote Note = ValidNote.Parse("Hey peeps ;)");
        private static readonly DateTime ScheduledDate = DateTime.UtcNow;
        private Mock<ICommandHandler<PostNoteCommand>> postNote;
        private Mock<ICommandHandler<ReviseNoteCommand>> reviseNote;
        private Mock<IRequesterContext> requesterContext;
        private Mock<IGuidCreator> guidCreator;
        private NotePostController target;

        [TestInitialize]
        public void Initialize()
        {
            this.postNote = new Mock<ICommandHandler<PostNoteCommand>>();
            this.reviseNote = new Mock<ICommandHandler<ReviseNoteCommand>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new NotePostController(
                this.postNote.Object,
                this.reviseNote.Object,
                this.requesterContext.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPostingNote_ItShouldIssuePostNoteCommand()
        {
            var data = new NewNoteData(ChannelId, Note.Value, ScheduledDate);
            var command = new PostNoteCommand(Requester, PostId, ChannelId, Note, ScheduledDate);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(PostId.Value);
            this.postNote.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostNote(data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.postNote.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPostingNote_WithoutSpecifyingNewNoteData_ItShouldThrowBadRequestException()
        {
            await this.target.PostNote(null);
        }

        [TestMethod]
        public async Task WhenPuttingNote_ItShouldIssuePostNoteCommand()
        {
            var data = new RevisedNoteData(ChannelId, Note.Value, ScheduledDate);
            var command = new ReviseNoteCommand(Requester, PostId, ChannelId, Note);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(PostId.Value);
            this.reviseNote.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PutNote(PostId.Value.EncodeGuid(), data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.postNote.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPuttingNote_WithoutSpecifyingRevisedNoteId_ItShouldThrowBadRequestException()
        {
            await this.target.PutNote(string.Empty, new RevisedNoteData(ChannelId, Note.Value, ScheduledDate));
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPuttingNote_WithoutSpecifyingRevisedNoteData_ItShouldThrowBadRequestException()
        {
            await this.target.PutNote(PostId.Value.EncodeGuid(), null);
        }
    }
}