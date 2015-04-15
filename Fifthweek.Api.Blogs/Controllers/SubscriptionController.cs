namespace Fifthweek.Api.Blogs.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("subscriptions"), AutoConstructor]
    public partial class SubscriptionController : ApiController
    {
        private readonly ICommandHandler<UpdateBlogSubscriptionsCommand> updateBlogSubscriptions;
        private readonly ICommandHandler<UnsubscribeFromChannelCommand> unsubscribeFromChannel;
        private readonly ICommandHandler<AcceptChannelSubscriptionPriceChangeCommand> acceptPriceChange;
        private readonly IRequesterContext requesterContext;

        [Route("blogs/{blogId}")]
        public async Task PutBlogSubscriptions(string blogId, [FromBody]UpdatedBlogSubscriptionData subscriptionData)
        {
            blogId.AssertUrlParameterProvided("blogId");
            subscriptionData.AssertBodyProvided("subscriptionData");
           
            var subscriptions = subscriptionData.Subscriptions
                .Select(v => v.Parse())
                .Select(v => new AcceptedChannelSubscription(new ChannelId(v.ChannelId.DecodeGuid()), v.AcceptedPrice)).ToList();

            var requester = this.requesterContext.GetRequester();
            var blogIdObject = new BlogId(blogId.DecodeGuid());
            
            await this.updateBlogSubscriptions.HandleAsync(new UpdateBlogSubscriptionsCommand(
                requester,
                blogIdObject,
                subscriptions));
        }

        [Route("channels/{channelId}")]
        public async Task DeleteChannelSubscription(string channelId)
        {
            channelId.AssertUrlParameterProvided("channelId");

            var requester = this.requesterContext.GetRequester();
            var channelIdObject = new ChannelId(channelId.DecodeGuid());
            
            await this.unsubscribeFromChannel.HandleAsync(new UnsubscribeFromChannelCommand(
                requester,
                channelIdObject));
        }

        [Route("channels/{channelId}")]
        public async Task PutChannelSubscription(string channelId, [FromBody]ChannelSubscriptionDataWithoutChannelId subscriptionData)
        {
            channelId.AssertUrlParameterProvided("channelId");
            subscriptionData.AssertBodyProvided("subscriptionData");

            var requester = this.requesterContext.GetRequester();
            var channelIdObject = new ChannelId(channelId.DecodeGuid());

            var parsedSubscriptionData = subscriptionData.Parse();
            var acceptedPrice = parsedSubscriptionData.AcceptedPrice;

            await this.acceptPriceChange.HandleAsync(new AcceptChannelSubscriptionPriceChangeCommand(
                requester,
                channelIdObject,
                acceptedPrice));
        }
    }
}