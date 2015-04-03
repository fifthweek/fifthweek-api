namespace Fifthweek.WebJobs.Thumbnails.Shared
{
    using System.Collections.Generic;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class CreateThumbnailsMessage
    {
        public CreateThumbnailsMessage()
        {
        }

        public FileId FileId { get; set; }

        public string ContainerName { get; set; }

        public string InputBlobName { get; set; }

        [Optional]
        public List<ThumbnailDefinition> Items { get; set; }
   
        public bool Overwrite { get; set; }
    }
}