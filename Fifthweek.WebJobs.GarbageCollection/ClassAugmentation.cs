using System;
using System.Linq;

//// Generated on 03/08/2015 10:59:40 (UTC)
//// Mapped solution in 17.44s


namespace Fifthweek.WebJobs.GarbageCollection
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.GarbageCollection;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.WebJobs.Shared;

    public partial class GarbageCollectionProcessor 
    {
        public GarbageCollectionProcessor(
            Fifthweek.GarbageCollection.IRunGarbageCollection runGarbageCollection)
        {
            if (runGarbageCollection == null)
            {
                throw new ArgumentNullException("runGarbageCollection");
            }

            this.runGarbageCollection = runGarbageCollection;
        }
    }
}



