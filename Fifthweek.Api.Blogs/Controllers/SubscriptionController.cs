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
    using Fifthweek.Shared;

    [RoutePrefix("subscriptions"), AutoConstructor]
    public partial class SubscriptionController : ApiController
    {
        private readonly ICommandHandler<UpdateBlogSubscriptionsCommand> updateBlogSubscriptions;
        private readonly ICommandHandler<UnsubscribeFromChannelCommand> unsubscribeFromChannel;
        private readonly ICommandHandler<SubscribeToChannelCommand> subscribeToChannel;
        private readonly IRequesterContext requesterContext;
        private readonly ITimestampCreator timestampCreator;

        [Route("blogs/{blogId}")]
        public async Task PutBlogSubscriptions(string blogId, [FromBody]UpdatedBlogSubscriptionData subscriptionData)
        {
            blogId.AssertUrlParameterProvided("blogId");
            subscriptionData.AssertBodyProvided("subscriptionData");

            var subscriptions = subscriptionData.Subscriptions
                .Select(v => v.Parse())
                .Select(v => new AcceptedChannelSubscription(new ChannelId(v.ChannelId.DecodeGuid()), v.AcceptedPrice)).ToList();

            var requester = await this.requesterContext.GetRequesterAsync();
            var blogIdObject = new BlogId(blogId.DecodeGuid());

            await this.updateBlogSubscriptions.HandleAsync(new UpdateBlogSubscriptionsCommand(
                requester,
                blogIdObject,
                subscriptions));
        }


        [Route("channels/{channelId}")]
        public async Task PostChannelSubscription(string channelId, [FromBody]ChannelSubscriptionDataWithoutChannelId subscriptionData)
        {
            channelId.AssertUrlParameterProvided("channelId");
            subscriptionData.AssertBodyProvided("subscriptionData");

            var requester = await this.requesterContext.GetRequesterAsync();
            var channelIdObject = new ChannelId(channelId.DecodeGuid());

            var parsedSubscriptionData = subscriptionData.Parse();
            var acceptedPrice = parsedSubscriptionData.AcceptedPrice;

            var now = this.timestampCreator.Now();

            await this.subscribeToChannel.HandleAsync(new SubscribeToChannelCommand(
                requester,
                channelIdObject,
                acceptedPrice,
                now));
        }

        [Route("channels/{channelId}")]
        public async Task DeleteChannelSubscription(string channelId)
        {
            channelId.AssertUrlParameterProvided("channelId");

            var requester = await this.requesterContext.GetRequesterAsync();
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

            var requester = await this.requesterContext.GetRequesterAsync();
            var channelIdObject = new ChannelId(channelId.DecodeGuid());

            var parsedSubscriptionData = subscriptionData.Parse();
            var acceptedPrice = parsedSubscriptionData.AcceptedPrice;

            var now = this.timestampCreator.Now();

            await this.subscribeToChannel.HandleAsync(new SubscribeToChannelCommand(
                requester,
                channelIdObject,
                acceptedPrice,
                now));
        }
    }
}