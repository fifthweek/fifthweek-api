namespace Fifthweek.WebJobs.Deletions.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeletePostMessage
    {
        public Guid PostId { get; private set; }

        public Guid FileId { get; private set; }

        public Guid ImageId { get; private set; }
    }
}