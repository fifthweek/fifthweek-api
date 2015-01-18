namespace Fifthweek.Api.FileManagement.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Controllers;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FileUploadControllerTests
    {
        private static readonly Guid GeneratedSqlGuid = Guid.NewGuid();
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly FileId FileId = new FileId(GeneratedSqlGuid);
        
        private Mock<IGuidCreator> guidCreator;
        private Mock<ICommandHandler<InitiateFileUploadCommand>> initiateFileUpload;
        private Mock<IQueryHandler<GenerateWritableBlobUriQuery, string>> generateWritableBlobUri;
        private Mock<ICommandHandler<CompleteFileUploadCommand>> completeFileUpload;
        private Mock<IUserContext> userContext;
        private FileUploadController target;

        [TestMethod]
        public async Task WhenAnUploadRequestIsPosted_ItShouldInitiateTheFileUploadAndGenerateAWritableBlobUri()
        {
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(GeneratedSqlGuid).Verifiable();

            const string UploadUri = "/test";
            const string FilePath = @"C:\test\myfile.jpeg";
            const string Purpose = "profile-picture";

            this.userContext.Setup(v => v.TryGetUserId()).Returns(UserId).Verifiable();
            this.initiateFileUpload.Setup(v => v.HandleAsync(new InitiateFileUploadCommand(Requester, FileId, FilePath, Purpose))).Returns(Task.FromResult(0)).Verifiable();
            this.generateWritableBlobUri.Setup(v => v.HandleAsync(new GenerateWritableBlobUriQuery(Requester, FileId, Purpose))).ReturnsAsync(UploadUri).Verifiable();

            var response = await this.target.PostUploadRequestAsync(new UploadRequest(FilePath, Purpose));

            this.guidCreator.Verify();
            this.userContext.Verify();
            this.initiateFileUpload.Verify();
            this.generateWritableBlobUri.Verify();

            Assert.AreEqual(new GrantedUpload(GeneratedSqlGuid.EncodeGuid(), UploadUri), response);
        }

        [TestMethod]
        public async Task WhenAnUploadCompleteNotificationIsPosted_ItShouldCompleteTheFileUpload()
        {
            this.userContext.Setup(v => v.TryGetUserId()).Returns(UserId).Verifiable();
            this.completeFileUpload.Setup(v => v.HandleAsync(new CompleteFileUploadCommand(Requester, FileId))).Returns(Task.FromResult(0)).Verifiable();

            await this.target.PostUploadCompleteNotificationAsync(GeneratedSqlGuid.EncodeGuid());

            this.userContext.Verify();
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

            this.target = new FileUploadController(
                this.guidCreator.Object,
                this.initiateFileUpload.Object,
                this.generateWritableBlobUri.Object,
                this.completeFileUpload.Object,
                this.userContext.Object);
        }
    }
}
