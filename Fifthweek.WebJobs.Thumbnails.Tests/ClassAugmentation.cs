using System;
using System.Linq;



namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System;
    using System.Linq;
    using System.IO;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    public partial class SampleImage 
    {
        public SampleImage(
            System.String path, 
            System.Int32 width, 
            System.Int32 height)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }

            if (width == null)
            {
                throw new ArgumentNullException("width");
            }

            if (height == null)
            {
                throw new ArgumentNullException("height");
            }

            this.Path = path;
            this.Width = width;
            this.Height = height;
        }
    }

}


