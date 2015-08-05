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
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Api.FileManagement.Shared.Constants;

    [TestClass]
    public class CompleteFileUploadCommandHandlerTests
    {
        private const string BlobName = "blobName";
        private const string ContainerName = "containerName";
        private const string Purpose = "purpose";
        private const string FileExtension = "jpeg";
        private const string MimeType = "image/jpeg";
        private const long BlobSize = 1234;
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());

        private Mock<IGetFileWaitingForUploadDbStatement> getFileWaitingForUpload;
        private Mock<ISetFileUploadCompleteDbStatement> setFileUploadComplete;
        private Mock<IMimeTypeMap> mimeTypeMap;
        private Mock<IBlobService> blobService;
        private Mock<IBlobLocationGenerator> blobNameCreator;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFileProcessor> fileProcessor;
        private Mock<IChannelSecurity> channelSecurity;

        private CompleteFileUploadCommandHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobNameCreator = new Mock<IBlobLocationGenerator>();
            this.mimeTypeMap = new Mock<IMimeTypeMap>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.getFileWaitingForUpload = new Mock<IGetFileWaitingForUploadDbStatement>();

            // Give side-effecting components strict mock behaviour.
            this.setFileUploadComplete = new Mock<ISetFileUploadCompleteDbStatement>(MockBehavior.Strict);
            this.blobService = new Mock<IBlobService>(MockBehavior.Strict);
            this.fileProcessor = new Mock<IFileProcessor>(MockBehavior.Strict);
            this.channelSecurity = new Mock<IChannelSecurity>(MockBehavior.Strict);

            this.handler = new CompleteFileUploadCommandHandler(
                this.getFileWaitingForUpload.Object,
                this.setFileUploadComplete.Object,
                this.mimeTypeMap.Object,
                this.blobService.Object,
                this.blobNameCreator.Object,
                this.requesterSecurity.Object,
                this.fileProcessor.Object,
                this.channelSecurity.Object);

            this.requesterSecurity.SetupFor(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.handler.HandleAsync(new CompleteFileUploadCommand(Requester.Unauthenticated, ChannelId, FileId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalledAndTheUserDoesNotOwnTheChannel_ItShouldFail()
        {
            this.channelSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, ChannelId))
                .Throws(new UnauthorizedException());

            await this.handler.HandleAsync(new CompleteFileUploadCommand(Requester, ChannelId, FileId));
        }

        [TestMethod]
        public async Task WhenCalledAndFileDoesNotBelogToUser_ItShouldNotUpdateStorageOrQueue()
        {
            this.channelSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, ChannelId))
                .Returns(Task.FromResult(0));

            this.getFileWaitingForUpload.Setup(v => v.ExecuteAsync(FileId))
                .ReturnsAsync(new FileWaitingForUpload(FileId, new UserId(Guid.NewGuid()), "myfile", FileExtension, Purpose));

            await ExpectedException.AssertExceptionAsync<UnauthorizedException>(() =>
            {
                return this.handler.HandleAsync(new CompleteFileUploadCommand(Requester, ChannelId, FileId));
            });
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldUpdateStorageAndQueue()
        {
            this.mimeTypeMap.Setup(_ => _.GetMimeType(FileExtension)).Returns(MimeType);

            this.channelSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, ChannelId))
                .Returns(Task.FromResult(0));

            this.getFileWaitingForUpload.Setup(v => v.ExecuteAsync(FileId))
                .ReturnsAsync(new FileWaitingForUpload(FileId, UserId, "myfile", FileExtension, Purpose));

            this.blobNameCreator.Setup(v => v.GetBlobLocation(ChannelId, FileId, Purpose))
                .Returns(new BlobLocation(ContainerName, BlobName));

            this.blobService.Setup(v => v.GetBlobLengthAndSetPropertiesAsync(ContainerName, BlobName, MimeType, Constants.PrivateReadSignatureTimeSpan + Constants.ReadSignatureMinimumExpiryTime))
                .ReturnsAsync(BlobSize).Verifiable();

            this.setFileUploadComplete.Setup(v => v.ExecuteAsync(FileId, BlobSize, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.fileProcessor.Setup(v => v.ProcessFileAsync(FileId, ContainerName, BlobName, Purpose))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.handler.HandleAsync(new CompleteFileUploadCommand(Requester, ChannelId, FileId));

            this.setFileUploadComplete.Verify();
            this.blobService.Verify();
            this.fileProcessor.Verify();
        }

        [TestMethod]
        public async Task WhenCalledWithNoChannelId_ItShouldNotCheckChannelPermissions()
        {
            this.mimeTypeMap.Setup(_ => _.GetMimeType(FileExtension)).Returns(MimeType);

            this.getFileWaitingForUpload.Setup(v => v.ExecuteAsync(FileId))
                .ReturnsAsync(new FileWaitingForUpload(FileId, UserId, "myfile", FileExtension, Purpose));

            this.blobNameCreator.Setup(v => v.GetBlobLocation(null, FileId, Purpose))
                .Returns(new BlobLocation(ContainerName, BlobName));

            this.blobService.Setup(v => v.GetBlobLengthAndSetPropertiesAsync(ContainerName, BlobName, MimeType, Constants.PrivateReadSignatureTimeSpan + Constants.ReadSignatureMinimumExpiryTime))
                .ReturnsAsync(BlobSize).Verifiable();

            this.setFileUploadComplete.Setup(v => v.ExecuteAsync(FileId, BlobSize, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.fileProcessor.Setup(v => v.ProcessFileAsync(FileId, ContainerName, BlobName, Purpose))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.handler.HandleAsync(new CompleteFileUploadCommand(Requester, null, FileId));

            this.setFileUploadComplete.Verify();
            this.blobService.Verify();
            this.fileProcessor.Verify();
        }
    }
}
