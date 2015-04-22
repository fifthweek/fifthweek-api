namespace Fifthweek.Api.Blogs.Queries
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

    [Obsolete]
    [AutoConstructor]
    public partial class ChannelsAndCollections
    {
        public IReadOnlyList<ChannelResult> Channels { get; private set; }
    }
}