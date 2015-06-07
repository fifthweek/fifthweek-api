using System;
using System.Linq;

//// Generated on 07/06/2015 15:55:42 (UTC)
//// Mapped solution in 17.25s


namespace Fifthweek.WebJobs.Snapshots
{
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.WebJobs.Shared;

    public partial class SnapshotProcessor 
    {
        public SnapshotProcessor(
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



