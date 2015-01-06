using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.FileManagement.Tests.Controllers
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Controllers;
    using Fifthweek.Api.FileManagement.Queries;

    using Moq;

    [TestClass]
    public class FileUploadControllerTests
    {
        [TestMethod]
        public void WhenAnUploadRequestIsPosted_ItShouldCreateStorageAndReturnSasUriIfValid()
        {
            ////this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(this.guid).Verifiable();

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
