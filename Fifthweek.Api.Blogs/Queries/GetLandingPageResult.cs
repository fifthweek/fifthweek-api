namespace Fifthweek.Api.Blogs.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetLandingPageResult
    {
        public UserId UserId { get; private set; }

        [Optional]
        public FileInformation ProfileImage { get; private set; }

        public BlogWithFileInformation Blog { get; private set; }

        public IReadOnlyList<ChannelResult> Channels { get; private set; }
    }
}