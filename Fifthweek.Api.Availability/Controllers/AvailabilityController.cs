namespace Fifthweek.Api.Availability.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;

    [RoutePrefix("availability")]
    public class AvailabilityController : ApiController
    {
        private readonly IQueryHandler<GetAvailabilityQuery, AvailabilityResult> getAvailability;

        public AvailabilityController(IQueryHandler<GetAvailabilityQuery, AvailabilityResult> getAvailability)
        {
            this.getAvailability = getAvailability;
        }

        // GET: availability
        [AllowAnonymous]
        [ResponseType(typeof(AvailabilityResult))]
        public async Task<HttpResponseMessage> Get()
        {
            var result = await this.getAvailability.HandleAsync(new GetAvailabilityQuery());
            
            if (!result.IsOk())
            {
                return this.Request.CreateResponse(HttpStatusCode.ServiceUnavailable, result);
            }

            return this.Request.CreateResponse(result);
        }

        // HEAD: availability
        [AllowAnonymous]
        [AcceptVerbs("HEAD")]
        public Task<HttpResponseMessage> Head()
        {
            return this.Get();
        }
    }
}
