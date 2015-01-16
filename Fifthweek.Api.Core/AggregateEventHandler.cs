using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fifthweek.Api.Core
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class AggregateEventHandler<T> : IEventHandler<T>
    {
        private readonly IEnumerable<IEventHandler<T>> eventHandlers;

        public Task HandleAsync(T @event)
        {
            var runningTasks = this.eventHandlers.Select(_ => _.HandleAsync(@event)).ToArray();
            return Task.WhenAll(runningTasks);
        }
    }
}