namespace Fifthweek.Api.Channels.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("collections"), AutoConstructor]
    public partial class ChannelController : ApiController
    {
        private readonly ICommandHandler<CreateChannelCommand> createChannel;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        [Route]
        public async Task<ChannelId> PostChannelAsync(NewChannelData newChannel)
        {
            newChannel.AssertBodyProvided("newChannel");
            newChannel.Parse();

            var requester = this.requesterContext.GetRequester();
            var newChannelId = new ChannelId(this.guidCreator.CreateSqlSequential());

            await this.createChannel.HandleAsync(
                new CreateChannelCommand(
                    requester,
                    newChannelId,
                    newChannel.SubscriptionId,
                    newChannel.NameObject,
                    newChannel.PriceObject));

            return newChannelId;
        }
    }
}