namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System;
    using System.IO;
    using System.Reflection;

    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class SampleImage
    {
        public string Path { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public Stream Open()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(this.Path);
        }
    }
}