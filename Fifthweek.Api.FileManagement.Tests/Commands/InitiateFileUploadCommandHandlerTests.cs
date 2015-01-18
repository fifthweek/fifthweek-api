namespace Fifthweek.Api.FileManagement.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.Identity.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class InitiateFileUploadCommandHandlerTests
    {
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
        public async Task WhenPassedValidInformation_ItShouldAddANewFile()
        {
            await this.TestCommandHandler(
                new FileId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
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
                new FileId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
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
                new FileId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
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
                new FileId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
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
                new FileId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
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
                new FileId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
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
                new FileId(Guid.NewGuid()),
                new UserId(Guid.NewGuid()),
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
