using System;
using System.Linq;



namespace Fifthweek.Api.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
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


