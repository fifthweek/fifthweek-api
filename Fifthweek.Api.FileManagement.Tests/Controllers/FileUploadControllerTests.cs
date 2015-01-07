using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.FileManagement.Tests.Controllers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Controllers;
    using Fifthweek.Api.FileManagement.Queries;

    using Moq;

    [TestClass]
    public class FileUploadControllerTests
    {
        [TestMethod]
        public async Task WhenAnUploadRequestIsPosted_ItShouldCreateStorageAndReturnSasUriIfValid()
        {
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(this.guid).Verifiable();

            var fileId = new FileId(this.guid);
            var uploadUri = "/test";

            this.initiateFileUploadRequest.Setup(v => v.HandleAsync(new InitiateFileUploadRequestCommand(fileId))).Returns(Task.FromResult(0)).Verifiable();
            this.getSharedAccessSignatureUri.Setup(v => v.HandleAsync(new GetSharedAccessSignatureUriQuery(fileId))).ReturnsAsync(uploadUri).Verifiable();

            var response = await this.fileUploadController.PostUploadRequestAsync(new UploadRequest());

            this.guidCreator.Verify();
            this.initiateFileUploadRequest.Verify();
            this.getSharedAccessSignatureUri.Verify();

            Assert.AreEqual(new GrantedUpload(this.guid, uploadUri), response);
        }

        [TestMethod]
        public async Task WhenAnUploadCompleteNotificationIsPosted_ItShouldHandleTheNotification()
        {
            var fileId = new FileId(this.guid);

            this.fileUploadComplete.Setup(v => v.HandleAsync(new FileUploadCompleteCommand(fileId))).Returns(Task.FromResult(0)).Verifiable();

            await this.fileUploadController.PostUploadCompleteNotificationAsync(this.guid);

            this.fileUploadComplete.Verify();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.guidCreator = new Mock<IGuidCreator>();
            this.initiateFileUploadRequest = new Mock<ICommandHandler<InitiateFileUploadRequestCommand>>();
            this.getSharedAccessSignatureUri = new Mock<IQueryHandler<GetSharedAccessSignatureUriQuery, string>>();
            this.fileUploadComplete = new Mock<ICommandHandler<FileUploadCompleteCommand>>();

            this.fileUploadController = new FileUploadController(
                this.guidCreator.Object,
                this.initiateFileUploadRequest.Object,
                this.getSharedAccessSignatureUri.Object,
                this.fileUploadComplete.Object);
        }

        private readonly Guid guid = Guid.NewGuid();

        private Mock<IGuidCreator> guidCreator;

        private Mock<ICommandHandler<InitiateFileUploadRequestCommand>> initiateFileUploadRequest;

        private Mock<IQueryHandler<GetSharedAccessSignatureUriQuery, string>> getSharedAccessSignatureUri;

        private Mock<ICommandHandler<FileUploadCompleteCommand>> fileUploadComplete;

        private FileUploadController fileUploadController;
    }
}
