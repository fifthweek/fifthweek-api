namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System;
    using System.Drawing;
    using System.IO;

    using Fifthweek.WebJobs.Thumbnails.Shared;

    using ImageMagick;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ImageServiceTests
    {
        private ImageService target;

        private static readonly Color SideColor = Color.FromArgb(255, 0, 255, 0);

        private static readonly Color TopColor = Color.FromArgb(255, 0, 0, 255);

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new ImageService();
        }


        [TestMethod]
        public void WhenBothDimensionsAreSmaller_ItShouldShrinkAndMaintainAspectRatio_Landscape()
        {
            var sampleImage = SampleImagesLoader.Instance.LargeLandscape;
            int desiredWidth = sampleImage.Height / 2;
            int desiredHeight = sampleImage.Height / 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionsAreSmaller_ItShouldShrinkAndMaintainAspectRatio_Portrait()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePortrait;
            int desiredWidth = sampleImage.Width / 2;
            int desiredHeight = sampleImage.Width / 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreNotEqual(desiredWidth, image.Width),
                image => Assert.AreEqual(desiredHeight, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionsAreSmaller_ItShouldShrinkAndMaintainAspectRatio_Panorama()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePanorama;
            int desiredWidth = sampleImage.Height / 2;
            int desiredHeight = sampleImage.Height / 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionsAreSmaller_ItShouldShrinkAndMaintainAspectRatio_ColoredEdges()
        {
            var sampleImage = SampleImagesLoader.Instance.ColoredEdges;
            int desiredWidth = sampleImage.Height / 2;
            int desiredHeight = sampleImage.Height / 3;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreNotEqual(desiredWidth, image.Width),
                image => Assert.AreEqual(desiredHeight, image.Height),
                image => Assert.AreEqual(SideColor, this.GetLeft(image)),
                image => Assert.AreEqual(SideColor, this.GetRight(image)),
                image => Assert.AreEqual(TopColor, this.GetTop(image)),
                image => Assert.AreEqual(TopColor, this.GetBottom(image)));
        }

        [TestMethod]
        public void WhenOneDimensionIsSmaller_ItShouldShrinkToTheSmallerSizeAndMaintainAspectRatio_Landscape()
        {
            var sampleImage = SampleImagesLoader.Instance.LargeLandscape;
            int desiredWidth = sampleImage.Width * 2;
            int desiredHeight = sampleImage.Height / 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreNotEqual(desiredWidth, image.Width),
                image => Assert.AreEqual(desiredHeight, image.Height));
        }

        [TestMethod]
        public void WhenOneDimensionIsSmaller_ItShouldShrinkToTheSmallerSizeAndMaintainAspectRatio_Portrait()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePortrait;
            int desiredWidth = sampleImage.Width / 2;
            int desiredHeight = sampleImage.Height * 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height));
        }

        [TestMethod]
        public void WhenOneDimensionIsSmaller_ItShouldShrinkToTheSmallerSizeAndMaintainAspectRatio_Panorama()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePanorama;
            int desiredWidth = sampleImage.Width / 2;
            int desiredHeight = sampleImage.Height * 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height));
        }

        [TestMethod]
        public void WhenOneDimensionIsSmaller_ItShouldShrinkToTheSmallerSizeAndMaintainAspectRatio_ColoredEdges()
        {
            var sampleImage = SampleImagesLoader.Instance.ColoredEdges;
            int desiredWidth = sampleImage.Width / 2;
            int desiredHeight = sampleImage.Height * 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height),
                image => Assert.AreNotEqual(desiredHeight, image.Height),
                image => Assert.AreEqual(SideColor, this.GetLeft(image)),
                image => Assert.AreEqual(SideColor, this.GetRight(image)),
                image => Assert.AreEqual(TopColor, this.GetTop(image)),
                image => Assert.AreEqual(TopColor, this.GetBottom(image)));
        }

        [TestMethod]
        public void WhenBothDimensionAreLargerOrSame_ItShouldNotChangeTheImageSize_Landscape()
        {
            var sampleImage = SampleImagesLoader.Instance.LargeLandscape;
            int desiredWidth = sampleImage.Width * 2;
            int desiredHeight = sampleImage.Height * 3;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreEqual(sampleImage.Width, image.Width),
                image => Assert.AreEqual(sampleImage.Height, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionAreLargerOrSame_ItShouldNotChangeTheImageSize_Portrait()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePortrait;
            int desiredWidth = sampleImage.Width;
            int desiredHeight = sampleImage.Height * 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreEqual(sampleImage.Width, image.Width),
                image => Assert.AreEqual(sampleImage.Height, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionAreLargerOrSame_ItShouldNotChangeTheImageSize_Panorama()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePanorama;
            int desiredWidth = sampleImage.Width;
            int desiredHeight = sampleImage.Height;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreEqual(sampleImage.Width, image.Width),
                image => Assert.AreEqual(sampleImage.Height, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionAreLargerOrSame_ItShouldNotChangeTheImageSize_ColoredEdges()
        {
            var sampleImage = SampleImagesLoader.Instance.ColoredEdges;
            int desiredWidth = sampleImage.Width * 3;
            int desiredHeight = sampleImage.Height * 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.MaintainAspectRatio,
                image => Assert.AreEqual(sampleImage.Width, image.Width),
                image => Assert.AreEqual(sampleImage.Height, image.Height),
                image => Assert.AreEqual(SideColor, this.GetLeft(image)),
                image => Assert.AreEqual(SideColor, this.GetRight(image)),
                image => Assert.AreEqual(TopColor, this.GetTop(image)),
                image => Assert.AreEqual(TopColor, this.GetBottom(image)));
        }

        [TestMethod]
        public void WhenBothDimensionsAreSmaller_ItShouldShrinkAndCropToAspectRatio_Landscape()
        {
            var sampleImage = SampleImagesLoader.Instance.LargeLandscape;
            int desiredWidth = sampleImage.Height / 2;
            int desiredHeight = sampleImage.Height / 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreEqual(desiredHeight, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionsAreSmaller_ItShouldShrinkAndCropToAspectRatio_Portrait()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePortrait;
            int desiredWidth = sampleImage.Width / 2;
            int desiredHeight = sampleImage.Width / 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreEqual(desiredHeight, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionsAreSmaller_ItShouldShrinkAndCropToAspectRatio_Panorama()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePanorama;
            int desiredWidth = sampleImage.Height / 2;
            int desiredHeight = sampleImage.Height / 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreEqual(desiredHeight, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionsAreSmaller_ItShouldShrinkAndCropToAspectRatio_ColoredEdges()
        {
            var sampleImage = SampleImagesLoader.Instance.ColoredEdges;
            int desiredWidth = sampleImage.Width / 8;
            int desiredHeight = sampleImage.Height / 4;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreEqual(desiredHeight, image.Height),
                image => Assert.AreNotEqual(SideColor, this.GetLeft(image)),
                image => Assert.AreNotEqual(SideColor, this.GetRight(image)),
                image => Assert.AreEqual(TopColor, this.GetTop(image)),
                image => Assert.AreEqual(TopColor, this.GetBottom(image)));
        }

        [TestMethod]
        public void WhenOneDimensionIsSmaller_ItShouldCropToAspectRatio_Landscape()
        {
            var sampleImage = SampleImagesLoader.Instance.LargeLandscape;
            int desiredWidth = sampleImage.Width * 2;
            int desiredHeight = sampleImage.Height / 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreNotEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height),
                image => Assert.AreEqual(sampleImage.Width, image.Width),
                image => Assert.IsTrue(sampleImage.Height > image.Height));
        }

        [TestMethod]
        public void WhenOneDimensionIsSmaller_ItShouldCropToAspectRatio_Portrait()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePortrait;
            int desiredWidth = sampleImage.Width / 2;
            int desiredHeight = sampleImage.Height * 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreNotEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height),
                image => Assert.AreEqual(sampleImage.Height, image.Height),
                image => Assert.IsTrue(sampleImage.Width > image.Width));
        }

        [TestMethod]
        public void WhenOneDimensionIsSmaller_ItShouldCropToAspectRatio_Panorama()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePanorama;
            int desiredWidth = sampleImage.Width / 2;
            int desiredHeight = sampleImage.Height;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreEqual(desiredWidth, image.Width),
                image => Assert.AreEqual(desiredHeight, image.Height),
                image => Assert.AreEqual(sampleImage.Height, image.Height),
                image => Assert.IsTrue(sampleImage.Width > image.Width));
        }

        [TestMethod]
        public void WhenOneDimensionIsSmaller_ItShouldCropToAspectRatio_ColoredEdges()
        {
            var sampleImage = SampleImagesLoader.Instance.ColoredEdges;
            int desiredWidth = sampleImage.Width * 2;
            int desiredHeight = sampleImage.Height / 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreNotEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height),
                image => Assert.AreEqual(sampleImage.Width, image.Width),
                image => Assert.IsTrue(sampleImage.Height > image.Height),
                image => Assert.AreEqual(SideColor, this.GetLeft(image)),
                image => Assert.AreEqual(SideColor, this.GetRight(image)),
                image => Assert.AreNotEqual(TopColor, this.GetTop(image)),
                image => Assert.AreNotEqual(TopColor, this.GetBottom(image)));
        }

        [TestMethod]
        public void WhenBothDimensionAreLargerOrSame_ItShouldCropToAspectRatio_Landscape()
        {
            var sampleImage = SampleImagesLoader.Instance.LargeLandscape;
            int desiredWidth = sampleImage.Width * 2;
            int desiredHeight = sampleImage.Height * 3;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreNotEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height),
                image => Assert.AreEqual(sampleImage.Height, image.Height),
                image => Assert.IsTrue(sampleImage.Width > image.Width));
        }

        [TestMethod]
        public void WhenBothDimensionAreLargerOrSame_ItShouldCropToAspectRatio_Portrait()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePortrait;
            int desiredWidth = sampleImage.Width;
            int desiredHeight = sampleImage.Height * 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreNotEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height),
                image => Assert.AreEqual(sampleImage.Height, image.Height),
                image => Assert.IsTrue(sampleImage.Width > image.Width));
        }

        [TestMethod]
        public void WhenBothDimensionAreLargerOrSame_ItShouldCropToAspectRatioPanorama()
        {
            var sampleImage = SampleImagesLoader.Instance.LargePanorama;
            int desiredWidth = sampleImage.Width;
            int desiredHeight = sampleImage.Height;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreEqual(sampleImage.Width, image.Width),
                image => Assert.AreEqual(sampleImage.Height, image.Height));
        }

        [TestMethod]
        public void WhenBothDimensionAreLargerOrSame_ItShouldCropToAspectRatio_ColoredEdges()
        {
            var sampleImage = SampleImagesLoader.Instance.ColoredEdges;
            int desiredWidth = sampleImage.Width * 4;
            int desiredHeight = sampleImage.Height * 2;

            this.PerformResizeTest(
                sampleImage,
                desiredWidth,
                desiredHeight,
                ResizeBehaviour.CropToAspectRatio,
                image => Assert.AreNotEqual(desiredWidth, image.Width),
                image => Assert.AreNotEqual(desiredHeight, image.Height),
                image => Assert.AreEqual(sampleImage.Width, image.Width),
                image => Assert.IsTrue(sampleImage.Height > image.Height),
                image => Assert.AreEqual(SideColor, this.GetLeft(image)),
                image => Assert.AreEqual(SideColor, this.GetRight(image)),
                image => Assert.AreNotEqual(TopColor, this.GetTop(image)),
                image => Assert.AreNotEqual(TopColor, this.GetBottom(image)));
        }

        private static void SaveImage(MagickImage image, int width, int height, ResizeBehaviour resizeBehaviour)
        {
            image.Write(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                    string.Format("{0}-{1}-{2}x{3}.jpeg", DateTime.UtcNow.Ticks, resizeBehaviour, width, height)));
        }

        private void PerformResizeTest(
            SampleImage sampleImage,
            int desiredWidth,
            int desiredHeight,
            ResizeBehaviour resizeBehaviour,
            params Action<Bitmap>[] asserts)
        {
            using (var stream = sampleImage.Open())
            using (var input = new MagickImage(stream))
            using (var output = new MemoryStream())
            {
                this.target.Resize(input, output, desiredWidth, desiredHeight, resizeBehaviour);
                ////SaveImage(input, desiredWidth, desiredHeight, resizeBehaviour);

                output.Seek(0, SeekOrigin.Begin);
                var image = new Bitmap(output);
                foreach (var assert in asserts)
                {
                    assert(image);
                }

                if (resizeBehaviour == ResizeBehaviour.MaintainAspectRatio)
                {
                    Assert.IsTrue(
                        this.AreClose(
                            sampleImage.Width / (double)sampleImage.Height,
                            image.Width / (double)image.Height));
                }
                else
                {
                    Assert.IsTrue(
                        this.AreClose(desiredWidth / (double)desiredHeight, image.Width / (double)image.Height));
                }
            }
        }

        private bool AreClose(double first, double second)
        {
            return Math.Abs(first - second) < 0.01;
        }

        private Color GetLeft(Bitmap image)
        {
            return image.GetPixel(0, image.Height / 2);
        }

        private Color GetRight(Bitmap image)
        {
            return image.GetPixel(image.Width - 1, image.Height / 2);
        }

        private Color GetTop(Bitmap image)
        {
            return image.GetPixel(image.Width / 2, 0);
        }

        private Color GetBottom(Bitmap image)
        {
            return image.GetPixel(image.Width / 2, image.Height - 1);
        }
    }
}