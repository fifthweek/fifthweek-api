namespace Fifthweek.Api.Blogs.Controllers
{
    using System.Collections.Generic;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class UpdatedBlogSubscriptionData
    {
        public List<ChannelSubscriptionDataWithChannelId> Subscriptions { get; set; }
    }
}