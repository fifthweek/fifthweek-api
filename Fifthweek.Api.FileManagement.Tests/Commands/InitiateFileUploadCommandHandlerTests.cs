namespace Fifthweek.Api.FileManagement.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.Identity.Membership;

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

        private Mock<IFileRepository> fileRepository;
        private Mock<IBlobService> blobService;
        private InitiateFileUploadCommandHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            // Give side-effecting components strict mock behaviour.
            this.blobService = new Mock<IBlobService>(MockBehavior.Strict);
            this.fileRepository = new Mock<IFileRepository>(MockBehavior.Strict);

            this.target = new InitiateFileUploadCommandHandler(this.blobService.Object, this.fileRepository.Object);
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
                string.Empty,
                string.Empty,
                "purpose");
        }

        private async Task TestCommandHandler(FileId fileId, UserId requester, string filePath, string purpose, string expectedFileName, string expectedExtension, string expectedPurpose)
        {
            this.blobService.Setup(v => v.CreateBlobContainerAsync(Constants.PublicFileBlobContainerName))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.fileRepository.Setup(v => v.AddNewFileAsync(fileId, requester, expectedFileName, expectedExtension, expectedPurpose))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new InitiateFileUploadCommand(Requester.Authenticated(requester), fileId, filePath, purpose));

            this.blobService.Verify();
            this.fileRepository.Verify();
        }
    }
}
