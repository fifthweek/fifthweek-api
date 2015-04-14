namespace Fifthweek.Api.Blogs.Queries
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetLandingPageResult
    {
        public BlogWithFileInformation Blog { get; private set; }

        public IReadOnlyList<ChannelResult> Channels { get; private set; }
    }
}