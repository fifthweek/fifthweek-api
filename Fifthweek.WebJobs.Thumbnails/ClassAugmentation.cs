using System;
using System.Linq;

//// Generated on 13/10/2015 10:57:48 (UTC)
//// Mapped solution in 18.9s


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
    using System.Collections.Generic;
    using System.Diagnostics;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

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
    using System.Collections.Generic;
    using System.Diagnostics;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class SetFileProcessingCompleteDbStatement 
    {
        public SetFileProcessingCompleteDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbConnectionFactory connectionFactory)
        {
            if (connectionFactory == null)
            {
                throw new ArgumentNullException("connectionFactory");
            }

            this.connectionFactory = connectionFactory;
        }
    }
}
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class CreateThumbnailSetResult 
    {
        public CreateThumbnailSetResult(
            System.Int32 renderWidth,
            System.Int32 renderHeight)
        {
            if (renderWidth == null)
            {
                throw new ArgumentNullException("renderWidth");
            }

            if (renderHeight == null)
            {
                throw new ArgumentNullException("renderHeight");
            }

            this.RenderWidth = renderWidth;
            this.RenderHeight = renderHeight;
        }
    }
}
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public partial class LoggingThumbnailProcessorWrapper 
    {
        public LoggingThumbnailProcessorWrapper(
            Fifthweek.WebJobs.Thumbnails.IThumbnailProcessor thumbnailProcessor,
            Fifthweek.WebJobs.Thumbnails.ISetFileProcessingCompleteDbStatement setFileProcessingComplete)
        {
            if (thumbnailProcessor == null)
            {
                throw new ArgumentNullException("thumbnailProcessor");
            }

            if (setFileProcessingComplete == null)
            {
                throw new ArgumentNullException("setFileProcessingComplete");
            }

            this.thumbnailProcessor = thumbnailProcessor;
            this.setFileProcessingComplete = setFileProcessingComplete;
        }
    }
}



