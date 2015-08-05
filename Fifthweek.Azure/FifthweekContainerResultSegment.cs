namespace Fifthweek.Azure
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.WindowsAzure.Storage.Blob;

    public class FifthweekContainerResultSegment : IContainerResultSegment
    {
        private readonly ContainerResultSegment segment;

        public FifthweekContainerResultSegment(ContainerResultSegment segment)
        {
            this.segment = segment;
        }

        public BlobContinuationToken ContinuationToken
        {
            get
            {
                return this.segment.ContinuationToken;
            }
        }

        public IReadOnlyList<ICloudBlobContainer> Results
        {
            get
            {
                return this.segment.Results.Select(v => new FifthweekCloudBlobContainer(v)).ToList();
            }
        }
    }
}