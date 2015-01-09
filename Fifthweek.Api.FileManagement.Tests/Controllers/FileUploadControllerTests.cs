using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.FileManagement.Tests.Controllers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Controllers;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;

    using Moq;

    [TestClass]
    public class FileUploadControllerTests
    {
        [TestMethod]
        public async Task WhenAnUploadRequestIsPosted_ItShouldInitiateTheFileUploadAndGenerateAWritableBlobUri()
        {
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(this.guid).Verifiable();

            var fileId = new FileId(this.guid);
            var uploadUri = "/test";
            var userId = new UserId(Guid.NewGuid());

            this.userContext.Setup(v => v.GetUserId()).Returns(userId).Verifiable();
            this.initiateFileUpload.Setup(v => v.HandleAsync(new InitiateFileUploadCommand(userId, fileId))).Returns(Task.FromResult(0)).Verifiable();
            this.generateWritableBlobUri.Setup(v => v.HandleAsync(new GenerateWritableBlobUriQuery(fileId))).ReturnsAsync(uploadUri).Verifiable();

            var response = await this.fileUploadController.PostUploadRequestAsync(new UploadRequest());

            this.guidCreator.Verify();
            this.userContext.Verify();
            this.initiateFileUpload.Verify();
            this.generateWritableBlobUri.Verify();

            Assert.AreEqual(new GrantedUpload(this.guid, uploadUri), response);
        }

        [TestMethod]
        public async Task WhenAnUploadCompleteNotificationIsPosted_ItShouldCompleteTheFileUpload()
        {
            var fileId = new FileId(this.guid);

            this.completeFileUpload.Setup(v => v.HandleAsync(new CompleteFileUploadCommand(fileId))).Returns(Task.FromResult(0)).Verifiable();

            await this.fileUploadController.PostUploadCompleteNotificationAsync(this.guid);

            this.completeFileUpload.Verify();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.guidCreator = new Mock<IGuidCreator>();
            this.initiateFileUpload = new Mock<ICommandHandler<InitiateFileUploadCommand>>();
            this.generateWritableBlobUri = new Mock<IQueryHandler<GenerateWritableBlobUriQuery, string>>();
            this.completeFileUpload = new Mock<ICommandHandler<CompleteFileUploadCommand>>();
            this.userContext = new Mock<IUserContext>();

            this.fileUploadController = new FileUploadController(
                this.guidCreator.Object,
                this.initiateFileUpload.Object,
                this.generateWritableBlobUri.Object,
                this.completeFileUpload.Object,
                this.userContext.Object);
        }

        private readonly Guid guid = Guid.NewGuid();

        private Mock<IGuidCreator> guidCreator;

        private Mock<ICommandHandler<InitiateFileUploadCommand>> initiateFileUpload;

        private Mock<IQueryHandler<GenerateWritableBlobUriQuery, string>> generateWritableBlobUri;

        private Mock<ICommandHandler<CompleteFileUploadCommand>> completeFileUpload;

        private Mock<IUserContext> userContext;

        private FileUploadController fileUploadController;
    }
}
