namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System.IO;

    using Fifthweek.Shared;

    using ImageMagick;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestMimeTypes
    {
        [TestMethod]
        public void WhenLoadingSampleImages_MagickImageMimeTypesShouldMatchMappedMimeTypes()
        {
            var mimeTypeMapper = new MimeTypeMap();

            foreach (var sampleImage in SampleImagesLoader.Instance.SampleImages)
            {
                using (var stream = sampleImage.Open())
                {
                    var image = new MagickImage(stream);
                    var extension = Path.GetExtension(sampleImage.Path);
                    var mimeType = mimeTypeMapper.GetMimeType(extension);
                    Assert.AreEqual(image.FormatInfo.MimeType, mimeType);
                }
            }
        }
    }
}