namespace Fifthweek.Api.FileManagement.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class InitiateFileUploadCommandHandlerTests
    {
        private const string FilePath = @"C:\test\myfile.jpeg";
        private const string Purpose = "purpose";

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private Mock<IAddNewFileDbStatement> addNewFileDbStatement;
        private Mock<IBlobService> blobService;
        private Mock<IBlobLocationGenerator> blobNameCreator;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IChannelSecurity> channelSecurity;
        private InitiateFileUploadCommandHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            // Give side-effecting components strict mock behaviour.
            this.blobService = new Mock<IBlobService>(MockBehavior.Strict);
            this.blobNameCreator = new Mock<IBlobLocationGenerator>();
            this.addNewFileDbStatement = new Mock<IAddNewFileDbStatement>(MockBehavior.Strict);
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.channelSecurity = new Mock<IChannelSecurity>(MockBehavior.Strict);
            this.requesterSecurity.SetupFor(Requester);

            this.target = new InitiateFileUploadCommandHandler(this.requesterSecurity.Object, this.channelSecurity.Object, this.blobService.Object, this.blobNameCreator.Object, this.addNewFileDbStatement.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new InitiateFileUploadCommand(Requester.Unauthenticated, ChannelId, FileId, FilePath, Purpose));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAuthorizedToWriteToChannel_ItShouldThrowUnauthorizedException()
        {
            this.channelSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, ChannelId)).Throws(new UnauthorizedException());

            await this.target.HandleAsync(new InitiateFileUploadCommand(Requester.Unauthenticated, ChannelId, FileId, FilePath, Purpose));
        }

        [TestMethod]
        public async Task WhenPassedValidInformation_ItShouldAddANewFile()
        {
            await this.TestCommandHandler(
                FileId,
                ChannelId,
                UserId,
                @"C:\test\myfile.jpeg",
                "purpose",
                FileManagement.Constants.PublicFileBlobContainerName,
                "myfile",
                "jpeg",
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedFilePathWithNoExtension_ItShouldAddANewFileWithNoExtension()
        {
            await this.TestCommandHandler(
                FileId,
                ChannelId,
                UserId,
                @"C:\test\myfile",
                "purpose",
                FileManagement.Constants.PublicFileBlobContainerName,
                "myfile",
                string.Empty,
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedHiddenFilePathWithNoExtension_ItShouldAddANewFileWithNoExtension()
        {
            await this.TestCommandHandler(
                FileId,
                ChannelId,
                UserId,
                @"C:\test\.myfile",
                "purpose",
                FileManagement.Constants.PublicFileBlobContainerName,
                "myfile",
                string.Empty,
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedFileNameWithNoExtension_ItShouldAddANewFileWithNoExtension()
        {
            await this.TestCommandHandler(
                FileId,
                ChannelId,
                UserId,
                @"hello",
                "purpose",
                FileManagement.Constants.PublicFileBlobContainerName,
                "hello",
                string.Empty,
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedFileWithMultiplePeriods_ItShouldExtractExtensionCorrectly()
        {
            await this.TestCommandHandler(
                FileId,
                ChannelId,
                UserId,
                @"C:\test\myfile.isgreat.jpeg",
                "purpose",
                FileManagement.Constants.PublicFileBlobContainerName,
                "myfile.isgreat",
                "jpeg",
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedFileWithNullPurpose_ItShouldAddANewFile()
        {
            await this.TestCommandHandler(
                FileId,
                ChannelId,
                UserId,
                @"C:\test\myfile.jpeg",
                null,
                FileManagement.Constants.PublicFileBlobContainerName,
                "myfile",
                "jpeg",
                string.Empty);
        }

        [TestMethod]
        public async Task WhenPassedNullFilePath_ItShouldAddANewFile()
        {
            await this.TestCommandHandler(
                FileId,
                ChannelId,
                UserId,
                null,
                "purpose",
                FileManagement.Constants.PublicFileBlobContainerName,
                string.Empty,
                string.Empty,
                "purpose");
        }

        [TestMethod]
        public async Task WhenPassedNullChannelId_ItShouldNotCheckChannelPermissions()
        {
            await this.TestCommandHandler(
                FileId,
                null,
                UserId,
                @"C:\test\myfile.jpeg",
                "purpose",
                FileManagement.Constants.PublicFileBlobContainerName,
                "myfile",
                "jpeg",
                "purpose");
        }

        [TestMethod]
        public async Task WhenAddingANonPublicFile_ItShouldAskForTheBlobContainerToBeCreated()
        {
            var containerName = Guid.NewGuid().ToString("N");

            this.blobService.Setup(v => v.CreateBlobContainerAsync(containerName))
              .Returns(Task.FromResult(0))
              .Verifiable();
            
            await this.TestCommandHandler(
                FileId,
                ChannelId,
                UserId,
                null,
                "purpose",
                containerName,
                string.Empty,
                string.Empty,
                "purpose");
        }

        private async Task TestCommandHandler(FileId fileId, ChannelId channelId, UserId requester, string filePath, string purpose, string blobContainerName, string expectedFileName, string expectedExtension, string expectedPurpose)
        {
            if (channelId != null)
            {
                this.channelSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, ChannelId))
                    .Returns(Task.FromResult(0));
            }

            this.blobNameCreator.Setup(v => v.GetBlobLocation(channelId, fileId, purpose))
                .Returns(new BlobLocation(blobContainerName, string.Empty));

            this.addNewFileDbStatement.Setup(v => v.ExecuteAsync(fileId, requester, expectedFileName, expectedExtension, expectedPurpose, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new InitiateFileUploadCommand(Requester, channelId, fileId, filePath, purpose));

            this.blobService.Verify();
            this.addNewFileDbStatement.Verify();
        }
    }
}
