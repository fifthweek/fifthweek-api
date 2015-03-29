namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System;
    using System.Collections.Generic;
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
        private static readonly string ContainerName = "containerName";
        private static readonly string InputBlobName = "inputBlob";
        private static readonly string OutputBlobName = "outputBlob";
        private static readonly string OutputBlobName2 = "outputBlob2";

        private static readonly CreateThumbnailsMessage Message = new CreateThumbnailsMessage(
            ContainerName,
            InputBlobName,
            new List<ThumbnailDefinition> 
            {
                new ThumbnailDefinition(
                    OutputBlobName,
                    200,
                    100,
                    ResizeBehaviour.MaintainAspectRatio,
                    null)
            },
            false);

        private static readonly CreateThumbnailsMessage OverwriteMessage = new CreateThumbnailsMessage(
            Message.ContainerName,
            Message.InputBlobName,
            new List<ThumbnailDefinition> 
            {
                new ThumbnailDefinition(
                    OutputBlobName,
                    200,
                    100,
                    ResizeBehaviour.MaintainAspectRatio,
                    null)
            },
            true);

        private static readonly CreateThumbnailsMessage MessageWithChild = new CreateThumbnailsMessage(
            ContainerName,
            InputBlobName,
            new List<ThumbnailDefinition> 
            {
                new ThumbnailDefinition(
                    OutputBlobName,
                    200,
                    100,
                    ResizeBehaviour.MaintainAspectRatio,
                    new List<ThumbnailDefinition>
                    {
                        new ThumbnailDefinition(
                            OutputBlobName2,
                            100,
                            50,
                            ResizeBehaviour.MaintainAspectRatio,
                            null)
                    })
            },
            false);

        private static readonly CreateThumbnailsMessage MessageWithSibling = new CreateThumbnailsMessage(
            ContainerName,
            InputBlobName,
            new List<ThumbnailDefinition> 
            {
                new ThumbnailDefinition(
                    OutputBlobName,
                    200,
                    100,
                    ResizeBehaviour.MaintainAspectRatio,
                    null),
                new ThumbnailDefinition(
                    OutputBlobName2,
                    100,
                    50,
                    ResizeBehaviour.MaintainAspectRatio,
                    null)
            },
            false);

        private static readonly Color PoisonColor = Color.FromArgb(Color.Black.A, Color.Black.R, Color.Black.G, Color.Black.B);

        private readonly MimeTypeMap mimeTypeMap = new MimeTypeMap();

        private Mock<IImageService> imageService;
        private Mock<ILogger> logger;
        private Mock<ICloudStorageAccount> storageAccount;
        private Mock<ICloudBlobClient> blobClient;
        private Mock<ICloudBlobContainer> container;
        private Mock<ICloudBlockBlob> input;
        private Mock<ICloudBlockBlob> output;
        private Mock<ICloudBlockBlob> output2;
        private Mock<IBlobProperties> outputProperties;
        private Mock<IBlobProperties> outputProperties2;
        private MockCloudBlobStream outputStream;
        private MockCloudBlobStream outputStream2;
        private ThumbnailProcessor target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.imageService = new Mock<IImageService>(MockBehavior.Strict);
            this.logger = new Mock<ILogger>();

            this.storageAccount = new Mock<ICloudStorageAccount>();
            this.blobClient = new Mock<ICloudBlobClient>();
            this.container = new Mock<ICloudBlobContainer>();
            this.output = new Mock<ICloudBlockBlob>();
            this.output2 = new Mock<ICloudBlockBlob>();

            this.input = new Mock<ICloudBlockBlob>();
            var inputProperties = new Mock<IBlobProperties>();
            inputProperties.Setup(v => v.CacheControl).Returns("cache-control");
            this.input.Setup(v => v.Properties).Returns(inputProperties.Object);

            this.storageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(this.blobClient.Object);
            this.blobClient.Setup(v => v.GetContainerReference(ContainerName)).Returns(this.container.Object);
            this.container.Setup(v => v.GetBlockBlobReference(OutputBlobName)).Returns(this.output.Object);
            this.container.Setup(v => v.GetBlockBlobReference(OutputBlobName2)).Returns(this.output2.Object);

            this.outputProperties = new Mock<IBlobProperties>();
            this.output.Setup(v => v.Properties).Returns(this.outputProperties.Object);
            this.output.Setup(v => v.SetPropertiesAsync(CancellationToken.None)).Returns(Task.FromResult(0));
            this.outputProperties.SetupProperty(v => v.ContentType, null);
            this.outputProperties.SetupProperty(v => v.CacheControl, null);

            this.outputProperties2 = new Mock<IBlobProperties>();
            this.output2.Setup(v => v.Properties).Returns(this.outputProperties2.Object);
            this.output2.Setup(v => v.SetPropertiesAsync(CancellationToken.None)).Returns(Task.FromResult(0));
            this.outputProperties2.SetupProperty(v => v.ContentType, null);
            this.outputProperties2.SetupProperty(v => v.CacheControl, null);

            this.outputStream = new MockCloudBlobStream();
            this.output.Setup(v => v.OpenWriteAsync(CancellationToken.None)).ReturnsAsync(this.outputStream);

            this.outputStream2 = new MockCloudBlobStream();
            this.output2.Setup(v => v.OpenWriteAsync(CancellationToken.None)).ReturnsAsync(this.outputStream2);

            this.target = new ThumbnailProcessor(this.imageService.Object);
        }

        [TestMethod]
        public async Task WhenCreatingNewThumbnail_ItShouldResizeTheImage()
        {
            int width = -1;
            int height = -1;
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream, Message.Items[0].Width, Message.Items[0].Height, Message.Items[0].ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) =>
                    {
                        width = a.Width;
                        height = a.Height;
                    })
                .Verifiable();

            using (var inputStream = SampleImagesLoader.Instance.LargeLandscape.Open())
            {
                this.input.Setup(v => v.OpenReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(inputStream);
                await this.target.CreateThumbnailSetAsync(
                        Message,
                        this.input.Object,
                        this.storageAccount.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Width, width);
            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Height, height);
            Assert.AreEqual(this.mimeTypeMap.GetMimeType(Path.GetExtension(SampleImagesLoader.Instance.LargeLandscape.Path)), this.outputProperties.Object.ContentType);
            Assert.AreEqual("cache-control", this.outputProperties.Object.CacheControl);
            Assert.IsTrue(this.outputStream.IsCommitted);
        }

        [TestMethod]
        public async Task WhenCreatingNewThumbnailsAsAncestors_ItShouldShareTheImageObject()
        {
            MagickImage first = null;
            int firstWidth = -1;
            int firstHeight = -1;
            var firstThumbnail = MessageWithChild.Items[0];
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream, firstThumbnail.Width, firstThumbnail.Height, firstThumbnail.ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) =>
                    { 
                        first = a;
                        firstWidth = a.Width;
                        firstHeight = a.Height;
                        a.Crop(c, d);
                    }).Verifiable();

            MagickImage second = null;
            int secondWidth = -1;
            int secondHeight = -1;
            var secondThumbnail = MessageWithChild.Items[0].Children[0];
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream2, secondThumbnail.Width, secondThumbnail.Height, secondThumbnail.ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) => 
                    { 
                        second = a;
                        secondWidth = a.Width;
                        secondHeight = a.Height;
                        a.Crop(c, d);
                    }).Verifiable();

            using (var inputStream = SampleImagesLoader.Instance.LargeLandscape.Open())
            {
                this.input.Setup(v => v.OpenReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(inputStream);
                await this.target.CreateThumbnailSetAsync(
                        MessageWithChild,
                        this.input.Object,
                        this.storageAccount.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.IsNotNull(first);
            Assert.IsNotNull(second);
            Assert.AreSame(first, second);

            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Width, firstWidth);
            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Height, firstHeight);
            Assert.AreEqual(MessageWithChild.Items[0].Width, secondWidth);
            Assert.AreEqual(MessageWithChild.Items[0].Height, secondHeight);
            Assert.IsTrue(this.outputStream.IsCommitted);
            Assert.IsTrue(this.outputStream2.IsCommitted);
        }

        [TestMethod]
        public async Task WhenCreatingNewThumbnailsAsSiblings_ItShouldNotShareTheImageObject()
        {
            MagickImage first = null;
            int firstWidth = -1;
            int firstHeight = -1;
            var firstThumbnail = MessageWithSibling.Items[0];
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream, firstThumbnail.Width, firstThumbnail.Height, firstThumbnail.ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) => 
                    {
                        first = a;
                        firstWidth = a.Width;
                        firstHeight = a.Height;
                        a.Crop(c, d);
                    }).Verifiable();

            MagickImage second = null;
            int secondWidth = -1;
            int secondHeight = -1;
            var secondThumbnail = MessageWithSibling.Items[1];
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream2, secondThumbnail.Width, secondThumbnail.Height, secondThumbnail.ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) => 
                    {
                        second = a;
                        secondWidth = a.Width;
                        secondHeight = a.Height;
                        a.Crop(c, d);
                    }).Verifiable();

            using (var inputStream = SampleImagesLoader.Instance.LargeLandscape.Open())
            {
                this.input.Setup(v => v.OpenReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(inputStream);
                await this.target.CreateThumbnailSetAsync(
                        MessageWithSibling,
                        this.input.Object,
                        this.storageAccount.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.IsNotNull(first);
            Assert.IsNotNull(second);
            Assert.AreNotSame(first, second);
            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Width, firstWidth);
            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Height, firstHeight);
            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Width, secondWidth);
            Assert.AreEqual(SampleImagesLoader.Instance.LargeLandscape.Height, secondHeight);
            Assert.IsTrue(this.outputStream.IsCommitted);
            Assert.IsTrue(this.outputStream2.IsCommitted);
        }

        [TestMethod]
        public async Task WhenCreatingExistingThumbnailWithOverwrite_ItShouldResizeTheImage()
        {
            this.output.Setup(v => v.ExistsAsync(CancellationToken.None)).ReturnsAsync(true);

            MagickImage image = null;
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream, Message.Items[0].Width, Message.Items[0].Height, Message.Items[0].ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>((a, b, c, d, e) => image = a)
                .Verifiable();

            using (var inputStream = SampleImagesLoader.Instance.LargeLandscape.Open())
            {
                this.input.Setup(v => v.OpenReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(inputStream);
                await this.target.CreateThumbnailSetAsync(
                        OverwriteMessage,
                        this.input.Object,
                        this.storageAccount.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.IsTrue(this.outputStream.IsCommitted);
        }

        [TestMethod]
        public async Task WhenCreatingExistingThumbnail_ItShouldNotResizeTheImage()
        {
            this.output.Setup(v => v.ExistsAsync(CancellationToken.None)).ReturnsAsync(true);

            using (var inputStream = SampleImagesLoader.Instance.LargeLandscape.Open())
            {
                this.input.Setup(v => v.OpenReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(inputStream);
                await this.target.CreateThumbnailSetAsync(
                        Message,
                        this.input.Object,
                        this.storageAccount.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            // ImageService is strictly mocked, so this will fail if it is called.
        }

        [TestMethod]
        public async Task WhenCreatingThumbnailFromUnsupportedOutputFormat_ItShouldConvertToJpeg()
        {
            int width = -1;
            int height = -1;
            var format = MagickFormat.Tiff;
            string mimeType = null;
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream, Message.Items[0].Width, Message.Items[0].Height, Message.Items[0].ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) =>
                    {
                        width = a.Width;
                        height = a.Height;
                        format = a.Format;
                        mimeType = a.FormatInfo.MimeType;
                    })
                .Verifiable();

            using (var inputStream = SampleImagesLoader.Instance.Tiff.Open())
            {
                this.input.Setup(v => v.OpenReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(inputStream);
                await this.target.CreateThumbnailSetAsync(
                        Message,
                        this.input.Object,
                        this.storageAccount.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.AreEqual(ThumbnailProcessor.DefaultOutputMimeType, this.outputProperties.Object.ContentType);
            Assert.AreEqual(MagickFormat.Jpeg, format);
        }

        [TestMethod]
        public async Task WhenCreatingThumbnailFromImageWithNoColorProfile_ItShouldAddColorProfile()
        {
            bool profileExists = false;
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream, Message.Items[0].Width, Message.Items[0].Height, Message.Items[0].ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) =>
                    {
                        profileExists = a.GetColorProfile() != null;
                    })
                .Verifiable();

            using (var inputStream = SampleImagesLoader.Instance.NoProfile.Open())
            {
                this.input.Setup(v => v.OpenReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(inputStream);
                await this.target.CreateThumbnailSetAsync(
                        Message,
                        this.input.Object,
                        this.storageAccount.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.IsTrue(profileExists);
        }

        [TestMethod]
        public async Task WhenCreatingThumbnailFromImageWithLowQuality_ItShouldNotAdjustQuality()
        {
            int quality = 0;
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream, Message.Items[0].Width, Message.Items[0].Height, Message.Items[0].ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) =>
                    {
                        quality = a.Quality;
                    })
                .Verifiable();

            using (var inputStream = SampleImagesLoader.Instance.LowQuality.Open())
            {
                this.input.Setup(v => v.OpenReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(inputStream);
                await this.target.CreateThumbnailSetAsync(
                        Message,
                        this.input.Object,
                        this.storageAccount.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.IsTrue(quality < 85);
        }

        [TestMethod]
        public async Task WhenCreatingThumbnailFromImageWithHighQuality_ItShouldSetQualityTo85()
        {
            int quality = 0;
            this.imageService.Setup(v => v.Resize(It.IsAny<MagickImage>(), this.outputStream, Message.Items[0].Width, Message.Items[0].Height, Message.Items[0].ResizeBehaviour))
                .Callback<MagickImage, Stream, int, int, ResizeBehaviour>(
                    (a, b, c, d, e) =>
                    {
                        quality = a.Quality;
                    })
                .Verifiable();

            using (var inputStream = SampleImagesLoader.Instance.HighQuality.Open())
            {
                this.input.Setup(v => v.OpenReadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(inputStream);
                await this.target.CreateThumbnailSetAsync(
                        Message,
                        this.input.Object,
                        this.storageAccount.Object,
                        this.logger.Object,
                        CancellationToken.None);
            }

            this.imageService.Verify();
            Assert.AreEqual(85, quality);
        }

        [TestMethod]
        public async Task WhenCreatingNewPoisonThumbnail_ItShouldCreateAnImage()
        {
            await this.target.CreatePoisonThumbnailSetAsync(
                    Message,
                    this.storageAccount.Object,
                    this.logger.Object,
                    CancellationToken.None);

            Assert.AreEqual(this.mimeTypeMap.GetMimeType("png"), this.outputProperties.Object.ContentType);
            Assert.IsTrue(this.outputStream.IsCommitted);

            var imageBytes = this.outputStream.ToArray();
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
            this.output.Setup(v => v.ExistsAsync(CancellationToken.None)).ReturnsAsync(true);

            await this.target.CreatePoisonThumbnailSetAsync(
                    OverwriteMessage,
                    this.storageAccount.Object,
                    this.logger.Object,
                    CancellationToken.None);

            Assert.AreEqual(this.mimeTypeMap.GetMimeType("png"), this.outputProperties.Object.ContentType);
            Assert.IsTrue(this.outputStream.IsCommitted);

            var imageBytes = this.outputStream.ToArray();
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
            this.output.Setup(v => v.OpenWriteAsync(CancellationToken.None))
                .Throws(new Exception("This should not be called"));

            this.output.Setup(v => v.ExistsAsync(CancellationToken.None)).ReturnsAsync(true);

            await this.target.CreatePoisonThumbnailSetAsync(
                    Message,
                    this.storageAccount.Object,
                    this.logger.Object,
                    CancellationToken.None);
        }
    }
}