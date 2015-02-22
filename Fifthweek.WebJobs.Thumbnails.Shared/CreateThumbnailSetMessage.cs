namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class CreateThumbnailSetMessage
    {
        public CreateThumbnailSetMessage()
        {
        }

        public string ContainerName { get; set; }

        public string InputBlobName { get; set; }

        [Optional]
        public List<ThumbnailSetItemMessage> Items { get; set; }
   
        public bool Overwrite { get; set; }
    }
}