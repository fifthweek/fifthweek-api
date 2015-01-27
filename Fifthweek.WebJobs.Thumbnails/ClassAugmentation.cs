using System;
using System.Linq;

namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Thumbnails.Shared;
    using ImageMagick;
    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Shared;
    using Microsoft.WindowsAzure.Storage.Blob;
    public partial class ThumbnailProcessor 
    {
        public ThumbnailProcessor(
            Fifthweek.WebJobs.Thumbnails.IImageService imageService)
        {
            if (imageService == null)
            {
                throw new ArgumentNullException("imageService");
            }

            this.imageService = imageService;
        }
    }

}


