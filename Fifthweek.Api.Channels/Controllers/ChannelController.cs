namespace Fifthweek.Api.Channels.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("channels"), AutoConstructor]
    public partial class ChannelController : ApiController
    {
        private readonly ICommandHandler<CreateChannelCommand> createChannel;
        private readonly ICommandHandler<UpdateChannelCommand> updateChannel;
        private readonly ICommandHandler<DeleteChannelCommand> deleteChannel;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        [Route]
        public async Task<ChannelId> PostChannelAsync(NewChannelData newChannelData)
        {
            newChannelData.AssertBodyProvided("newChannel");
            var newChannel = newChannelData.Parse();

            var requester = this.requesterContext.GetRequester();
            var newChannelId = new ChannelId(this.guidCreator.CreateSqlSequential());

            await this.createChannel.HandleAsync(
                new CreateChannelCommand(
                    requester,
                    newChannelId,
                    newChannel.SubscriptionId,
                    newChannel.Name,
                    newChannel.Price));

            return newChannelId;
        }

        [Route("{channelId}")]
        public async Task<IHttpActionResult> PutChannelAsync(string channelId, [FromBody]UpdatedChannelData channelData)
        {
            channelId.AssertUrlParameterProvided("channelId");
            channelData.AssertBodyProvided("channelData");
            var channel = channelData.Parse();

            var requester = this.requesterContext.GetRequester();
            var channelIdObject = new ChannelId(channelId.DecodeGuid());

            await this.updateChannel.HandleAsync(
                new UpdateChannelCommand(
                    requester,
                    channelIdObject,
                    channel.Name,
                    channel.Description,
                    channel.Price,
                    channel.IsVisibleToNonSubscribers));

            return this.Ok();
        }

        [Route("{channelId}")]
        public async Task<IHttpActionResult> DeleteChannelAsync(string channelId)
        {
            channelId.AssertUrlParameterProvided("channelId");

            var requester = this.requesterContext.GetRequester();
            var channelIdObject = new ChannelId(channelId.DecodeGuid());

            await this.deleteChannel.HandleAsync(new DeleteChannelCommand(requester, channelIdObject));

            return this.Ok();
        }
    }
}