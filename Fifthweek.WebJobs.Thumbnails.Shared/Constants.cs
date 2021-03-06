﻿namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System.Collections.Generic;

    public class Constants
    {
        public const string ThumbnailsQueueName = "create-thumbnail-requests";

        public static readonly HashSet<string> SupportedMimeTypes = new HashSet<string> 
        {
            "image/tiff",
            "image/png",
            "image/jpeg",
            "image/gif",
        };
    }
}