namespace Fifthweek.Api.FileManagement.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Api.FileManagement.Constants;

    [TestClass]
    public class InitiateFileUploadCommandHandlerTests
    {
        private const string FilePath = @"C:\test\myfile.jpeg";
        private const string Purpose = "purpose";

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private Mock<IFileRepository> fileRepository;
        private Mock<IBlobService> blobService;
        private Mock<IBlobLocationGenerator> blobNameCreator;
        private Mock<IRequesterSecurity> requesterSecurity;
        private InitiateFileUploadCommandHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            // Give side-effecting components strict mock behaviour.
            this.blobService = new Mock<IBlobService>(MockBehavior.Strict);
            this.blobNameCreator = new Mock<IBlobLocationGenerator>();
            this.fileRepository = new Mock<IFileRepository>(MockBehavior.Strict);
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            this.target = new InitiateFileUploadCommandHandler(this.requesterSecurity.Object, this.blobService.Object, this.blobNameCreator.Object, this.fileRepository.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new InitiateFileUploadCommand(Requester.Unauthenticated, FileId, FilePath, Purpose));
        }

        [TestMethod]
        public async Task WhenPassedValidInformation_ItShouldAddANewFile()
        {
            await this.TestCommandHandler(
                FileId,
                UserId,
                @"C:\test\myfile.jpeg",
                "purpose",
                Constants.PublicFileBlobContainerName,
                "myfile",
                "jpeg",
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedFilePathWithNoExtension_ItShouldAddANewFileWithNoExtension()
        {
            await this.TestCommandHandler(
                FileId,
                UserId,
                @"C:\test\myfile",
                "purpose",
                Constants.PublicFileBlobContainerName,
                "myfile",
                string.Empty,
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedHiddenFilePathWithNoExtension_ItShouldAddANewFileWithNoExtension()
        {
            await this.TestCommandHandler(
                FileId,
                UserId,
                @"C:\test\.myfile",
                "purpose",
                Constants.PublicFileBlobContainerName,
                "myfile",
                string.Empty,
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedFileNameWithNoExtension_ItShouldAddANewFileWithNoExtension()
        {
            await this.TestCommandHandler(
                FileId,
                UserId,
                @"hello",
                "purpose",
                Constants.PublicFileBlobContainerName,
                "hello",
                string.Empty,
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedFileWithMultiplePeriods_ItShouldExtractExtensionCorrectly()
        {
            await this.TestCommandHandler(
                FileId,
                UserId,
                @"C:\test\myfile.isgreat.jpeg",
                "purpose",
                Constants.PublicFileBlobContainerName,
                "myfile.isgreat",
                "jpeg",
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedFileWithNullPurpose_ItShouldAddANewFile()
        {
            await this.TestCommandHandler(
                FileId,
                UserId,
                @"C:\test\myfile.jpeg",
                null,
                Constants.PublicFileBlobContainerName,
                "myfile",
                "jpeg",
                string.Empty);
        }

        [TestMethod]
        public async Task WhenPassedNullFilePath_ItShouldAddANewFile()
        {
            await this.TestCommandHandler(
                FileId,
                UserId,
                null,
                "purpose",
                Constants.PublicFileBlobContainerName,
                string.Empty,
                string.Empty,
                "purpose");
        }

        [TestMethod]
        public async Task WhenAddingAPublicFile_ItShouldNotAskForTheBlobContainerToBeCreated()
        {
            var containerName = Guid.NewGuid().ToString("N");

            this.blobService.Setup(v => v.CreateBlobContainerAsync(containerName))
              .Returns(Task.FromResult(0))
              .Verifiable();
            
            await this.TestCommandHandler(
                FileId,
                UserId,
                null,
                "purpose",
                containerName,
                string.Empty,
                string.Empty,
                "purpose");
        }

        private async Task TestCommandHandler(FileId fileId, UserId requester, string filePath, string purpose, string blobContainerName, string expectedFileName, string expectedExtension, string expectedPurpose)
        {
            this.blobNameCreator.Setup(v => v.GetBlobLocation(requester, fileId, purpose))
                .Returns(new BlobLocation(blobContainerName, string.Empty));

            this.fileRepository.Setup(v => v.AddNewFileAsync(fileId, requester, expectedFileName, expectedExtension, expectedPurpose))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new InitiateFileUploadCommand(Requester, fileId, filePath, purpose));

            this.blobService.Verify();
            this.fileRepository.Verify();
        }
    }
}
