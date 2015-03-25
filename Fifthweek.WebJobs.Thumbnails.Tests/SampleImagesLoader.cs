namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class SampleImagesLoader
    {
        private static readonly string SampleImageDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SampleImages");

        public static readonly SampleImagesLoader Instance = new SampleImagesLoader();

        private readonly string[] excludedFileNames =
        {
            "readme",
            "readme.txt",
            "summary.txt",
            // The following can't be opened currently by ImageMagick.
            "flower-rgb-planar-02.tif",
            "flower-rgb-planar-04.tif",
            "flower-rgb-planar-10.tif",
            "flower-rgb-planar-12.tif",
            "flower-rgb-planar-14.tif",
            "flower-rgb-planar-24.tif",
            "flower-rgb-planar-32.tif",
            "off_l16.tif",
            "off_luv24.tif",
            "off_luv32.tif",
            "text.tif",
        };

        private SampleImagesLoader()
        {
           this.GetSampleImages();
        }

        public IList<SampleImage> SampleImages { get; private set; }

        public SampleImage LargeLandscape { get; private set; }

        public SampleImage LargePortrait { get; private set; }

        public SampleImage LargePanorama { get; private set; }

        public SampleImage ColoredEdges { get; private set; }

        public SampleImage Tiff { get; private set; }

        public SampleImage NoProfile { get; private set; }
        
        public SampleImage LowQuality { get; private set; }
        
        public SampleImage HighQuality { get; private set; }

        private void GetSampleImages()
        {
            var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var validResources = new List<SampleImage>();
            foreach (var path in resources)
            {
                if (this.excludedFileNames.Any(v => this.IsFile(path, v)))
                {
                    continue;
                }

                validResources.Add(this.CreateSampleImage(path));
            }

            Trace.WriteLine("Found " + validResources.Count + " sample images.");
            this.SampleImages = validResources.AsReadOnly();
        }

        private SampleImage CreateSampleImage(string path)
        {
            if (this.IsFile(path, "hong-kong-large.jpg"))
            {
                return this.LargeLandscape = new SampleImage(path, 1000, 665);
            }

            if (this.IsFile(path, "derweze-large-wide.jpg"))
            {
                return this.LargePanorama = new SampleImage(path, 2500, 671);
            }

            if (this.IsFile(path, "umbrella-large-portrait.jpg"))
            {
                return this.LargePortrait = new SampleImage(path, 665, 1000);
            }

            if (this.IsFile(path, "colored-edges.png"))
            {
                return this.ColoredEdges = new SampleImage(path, 1000, 1000);
            }

            if (this.IsFile(path, "pc260001.tif"))
            {
                return this.Tiff = new SampleImage(path, 640, 480);
            }

            if (this.IsFile(path, "trees-no-profile.jpg"))
            {
                return this.NoProfile = new SampleImage(path, 640, 480);
            }

            if (this.IsFile(path, "trees-quality-50.jpg"))
            {
                return this.LowQuality = new SampleImage(path, 640, 480);
            }

            if (this.IsFile(path, "trees-quality-95.jpg"))
            {
                return this.HighQuality = new SampleImage(path, 640, 480);
            }

            return new SampleImage(path, 0, 0);
        }

        private bool IsFile(string path, string fileName)
        {
            return path.ToLower().EndsWith("." + fileName.ToLower());
        }
    }
}