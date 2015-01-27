namespace Fifthweek.Api.Channels.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class ChannelId
    {
        public Guid Value { get; private set; }
    }
}