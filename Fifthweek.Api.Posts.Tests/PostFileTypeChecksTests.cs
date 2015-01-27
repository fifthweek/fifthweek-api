namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    

    [TestClass]
    public class PostFileTypeChecksTests
    {
        private const string FileExtension = "jpeg";
        private const string ImageMimeType = "image/"; // Not really a MIME type, but demonstrates support for all images.
        private const string NonImageMimeType = "imagey/something.else"; // Not a MIME type, but demonstrates support for types that are a substring of the image type.

        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private Mock<IGetFileExtensionDbStatement> getFileExtension;
        private Mock<IMimeTypeMap> mimeTypeMap;
        private PostFileTypeChecks target;

        [TestInitialize]
        public void Initialize()
        {
            this.getFileExtension = new Mock<IGetFileExtensionDbStatement>();
            this.mimeTypeMap = new Mock<IMimeTypeMap>();
            this.target = new PostFileTypeChecks(this.getFileExtension.Object, this.mimeTypeMap.Object);
        }

        [TestMethod]
        public async Task WhenValidatingFile_ItShouldAlwaysPass()
        {
            var result = await this.target.IsValidForFilePostAsync(FileId);

            Assert.IsTrue(result);

            await this.target.AssertValidForFilePostAsync(FileId);
        }

        [TestMethod]
        public async Task WhenValidatingImage_ItShouldPassForImagePrefixedMimeTypes()
        {
            this.mimeTypeMap.Setup(_ => _.GetMimeType(FileExtension)).Returns(ImageMimeType);
            this.getFileExtension.Setup(_ => _.ExecuteAsync(FileId)).ReturnsAsync(FileExtension);

            var result = await this.target.IsValidForImagePostAsync(FileId);

            Assert.IsTrue(result);

            await this.target.AssertValidForImagePostAsync(FileId);
        }

        [TestMethod]
        public async Task WhenValidatingImage_ItShouldFailForNonImagePrefixedMimeTypes()
        {
            this.mimeTypeMap.Setup(_ => _.GetMimeType(FileExtension)).Returns(NonImageMimeType);
            this.getFileExtension.Setup(_ => _.ExecuteAsync(FileId)).ReturnsAsync(FileExtension);

            var result = await this.target.IsValidForImagePostAsync(FileId);

            Assert.IsFalse(result);

            Func<Task> badMethodCall = () => this.target.AssertValidForImagePostAsync(FileId);

            await badMethodCall.AssertExceptionAsync<RecoverableException>();
        }
    }
}