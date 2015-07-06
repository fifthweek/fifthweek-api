using System;
using System.Linq;

//// Generated on 02/07/2015 14:19:25 (UTC)
//// Mapped solution in 13.98s


namespace Fifthweek.Api.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public partial class AggregateEventHandler<T> 
    {
        public AggregateEventHandler(
            System.Collections.Generic.IEnumerable<Fifthweek.Api.Core.IEventHandler<T>> eventHandlers)
        {
            if (eventHandlers == null)
            {
                throw new ArgumentNullException("eventHandlers");
            }

            this.eventHandlers = eventHandlers;
        }
    }
}


