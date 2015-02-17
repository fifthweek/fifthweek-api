namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using ImageMagick;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ThumbnailProcessorTests
    {
        private static readonly CreateThumbnailMessage Message = new CreateThumbnailMessage(
            "containerName",
            "inputBlob",
            "outputBlob",
            123,
            456,
            ResizeBehaviour.MaintainAspectRatio,
            false);
  
        private static readonly CreateThumbnailMessage OverwriteMessage = new CreateThumbnailMessage(
            Message.ContainerName,
            Message.InputBlobName,
            Message.OutputBlobName,
            Message.Width,
            Message.Height,
            Message.ResizeBehaviour,
            true);

        private static readonly Color PoisonColor = Color.FromArgb(Color.Black.A, Color.Black.R, Color.Black.G, Color.Black.B);

        private Mock<IImageService> imageService;

        private Mock<ILogger> logger;

        private Mock<ICloudBlockBlob> output;

        private ThumbnailProcessor target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.imageService = new Mock<IImageService>(MockBehavior.Strict);
            this.output = new Mock<ICloudBlockBlob>();
            this.logger = new Mock<ILogger>();

            this.target = new ThumbnailProcessor(this.imageService.Object);
        }

        [TestMethod]
        public async Task WhenCreatingNewThumbnail_ItShouldResizeTheImage()
        {
            var mimeTypeMap = new MimeTypeMap();

            var properties = new Mock<IBlobProperties>();
            this.output.Setup(v => v.Properties).Returns(properties.Object);
            this.output.Setup(v => v.SetPropertiesAsync(CancellationToken.None)).Returns(Task.FromResult(0));

            var outputStream = new MockCloudBlobStream();
            this.output.Setup(v => v.OpenWriteAsync(CancellationToken.None)).ReturnsAsync(outputStream);

            properties.SetupProperty(v => v.ContentType, null);

            int width = -1;
            int height = -1;
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), outputStream, Message.Width, Message.Height, Message.ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) =>
                    {
                        width = a.Width;
                        height = a.Height;
                    })
                .Verifiable();

            using (var input = SampleImagesLoader.Instance.LargeLandscape.Open())
            {
                await this.target.CreateThumbnailAsync(
                        Message,
                        input,
                        this.output.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Width, width);
            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Height, height);
            Assert.AreEqual(mimeTypeMap.GetMimeType(Path.GetExtension(SampleImagesLoader.Instance.LargeLandscape.Path)), properties.Object.ContentType);
            Assert.IsTrue(outputStream.IsCommitted);
        }

        [TestMethod]
        public async Task WhenCreatingExistingThumbnailWithOverwrite_ItShouldResizeTheImage()
        {
            var properties = new Mock<IBlobProperties>();
            this.output.Setup(v => v.Properties).Returns(properties.Object);
            this.output.Setup(v => v.SetPropertiesAsync(CancellationToken.None)).Returns(Task.FromResult(0));

            this.output.Setup(v => v.ExistsAsync(CancellationToken.None)).ReturnsAsync(true);

            var outputStream = new MockCloudBlobStream();
            this.output.Setup(v => v.OpenWriteAsync(CancellationToken.None)).ReturnsAsync(outputStream);

            MagickImage image = null;
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), outputStream, Message.Width, Message.Height, Message.ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>((a, b, c, d, e) => image = a)
                .Verifiable();

            using (var input = SampleImagesLoader.Instance.LargeLandscape.Open())
            {
                await this.target.CreateThumbnailAsync(
                        OverwriteMessage,
                        input,
                        this.output.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.IsTrue(outputStream.IsCommitted);
        }

        [TestMethod]
        public async Task WhenCreatingExistingThumbnail_ItShouldNotResizeTheImage()
        {
            var properties = new Mock<IBlobProperties>();
            this.output.Setup(v => v.Properties).Returns(properties.Object);

            this.output.Setup(v => v.ExistsAsync(CancellationToken.None)).ReturnsAsync(true);

            using (var input = SampleImagesLoader.Instance.LargeLandscape.Open())
            {
                await this.target.CreateThumbnailAsync(
                        Message,
                        input,
                        this.output.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            // ImageService is strictly mocked, so this will fail if it is called.
        }

        [TestMethod]
        public async Task WhenCreatingNewPoisonThumbnail_ItShouldCreateAnImage()
        {
            var mimeTypeMap = new MimeTypeMap();

            var properties = new Mock<IBlobProperties>();
            this.output.Setup(v => v.Properties).Returns(properties.Object);
            this.output.Setup(v => v.SetPropertiesAsync(CancellationToken.None)).Returns(Task.FromResult(0));

            var outputStream = new MockCloudBlobStream();
            this.output.Setup(v => v.OpenWriteAsync(CancellationToken.None)).ReturnsAsync(outputStream);

            properties.SetupProperty(v => v.ContentType, null);

            await this.target.CreatePoisonThumbnailAsync(
                    Message,
                    this.output.Object,
                    this.logger.Object,
                    CancellationToken.None);

            Assert.AreEqual(mimeTypeMap.GetMimeType("png"), properties.Object.ContentType);
            Assert.IsTrue(outputStream.IsCommitted);

            var imageBytes = outputStream.ToArray();
            Trace.WriteLine("Poison image is " + imageBytes.Length + " bytes.");

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                var image = new Bitmap(memoryStream);
                Assert.AreEqual(1, image.Width);
                Assert.AreEqual(1, image.Height);
                Assert.AreEqual(PoisonColor, image.GetPixel(0, 0));
            }
        }

        [TestMethod]
        public async Task WhenCreatingExistingPoisonThumbnailWithOverwrite_ItShouldCreateAnImage()
        {
            var mimeTypeMap = new MimeTypeMap();

            var properties = new Mock<IBlobProperties>();
            this.output.Setup(v => v.Properties).Returns(properties.Object);
            this.output.Setup(v => v.SetPropertiesAsync(CancellationToken.None)).Returns(Task.FromResult(0));

            var outputStream = new MockCloudBlobStream();
            this.output.Setup(v => v.OpenWriteAsync(CancellationToken.None)).ReturnsAsync(outputStream);

            this.output.Setup(v => v.ExistsAsync(CancellationToken.None)).ReturnsAsync(true);
            properties.SetupProperty(v => v.ContentType, null);

            await this.target.CreatePoisonThumbnailAsync(
                    OverwriteMessage,
                    this.output.Object,
                    this.logger.Object,
                    CancellationToken.None);

            Assert.AreEqual(mimeTypeMap.GetMimeType("png"), properties.Object.ContentType);
            Assert.IsTrue(outputStream.IsCommitted);

            var imageBytes = outputStream.ToArray();
            Trace.WriteLine("Poison image is " + imageBytes.Length + " bytes.");

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                var image = new Bitmap(memoryStream);
                Assert.AreEqual(1, image.Width);
                Assert.AreEqual(1, image.Height);
                Assert.AreEqual(PoisonColor, image.GetPixel(0, 0));
            }
        }

        [TestMethod]
        public async Task WhenCreatingExistingPoisonThumbnailWithNoOverwrite_ItShouldNotCreateAnImage()
        {
            var mimeTypeMap = new MimeTypeMap();

            var properties = new Mock<IBlobProperties>();
            this.output.Setup(v => v.Properties).Returns(properties.Object);
            this.output.Setup(v => v.SetPropertiesAsync(CancellationToken.None)).Returns(Task.FromResult(0));

            this.output.Setup(v => v.OpenWriteAsync(CancellationToken.None))
                .Throws(new Exception("This should not be called"));

            this.output.Setup(v => v.ExistsAsync(CancellationToken.None)).ReturnsAsync(true);

            await this.target.CreatePoisonThumbnailAsync(
                    Message,
                    this.output.Object,
                    this.logger.Object,
                    CancellationToken.None);
        }
    }
}