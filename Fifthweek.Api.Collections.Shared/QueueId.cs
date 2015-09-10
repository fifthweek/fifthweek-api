namespace Fifthweek.Api.Collections.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class QueueId
    {
        public Guid Value { get; private set; }
    }
}