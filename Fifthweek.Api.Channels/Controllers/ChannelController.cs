namespace Fifthweek.Api.Channels.Controllers
{
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("collections"), AutoConstructor]
    public partial class ChannelController : ApiController
    {
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

    }
}