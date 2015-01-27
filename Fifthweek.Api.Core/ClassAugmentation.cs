using System;
using System.Linq;




namespace Fifthweek.Api.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System;
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

