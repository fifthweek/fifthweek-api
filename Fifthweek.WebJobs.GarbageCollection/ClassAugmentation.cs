using System;
using System.Linq;

//// Generated on 06/08/2015 11:18:51 (UTC)
//// Mapped solution in 15.61s


namespace Fifthweek.WebJobs.GarbageCollection
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.GarbageCollection;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.Azure;

    public partial class GarbageCollectionProcessor 
    {
        public GarbageCollectionProcessor(
            Fifthweek.GarbageCollection.IRunGarbageCollection runGarbageCollection,
            Fifthweek.Azure.IBlobLeaseFactory blobLeaseFactory)
        {
            if (runGarbageCollection == null)
            {
                throw new ArgumentNullException("runGarbageCollection");
            }

            if (blobLeaseFactory == null)
            {
                throw new ArgumentNullException("blobLeaseFactory");
            }

            this.runGarbageCollection = runGarbageCollection;
            this.blobLeaseFactory = blobLeaseFactory;
        }
    }
}



