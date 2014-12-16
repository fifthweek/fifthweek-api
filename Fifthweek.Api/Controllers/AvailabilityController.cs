using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fifthweek.Api.Controllers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Models;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;

    [RoutePrefix("availability")]
    public class AvailabilityController : ApiController
    {
        private readonly IQueryHandler<GetAvailabilityQuery, AvailabilityResult> getAvailability;

        public AvailabilityController(IQueryHandler<GetAvailabilityQuery, AvailabilityResult> getAvailability)
        {
            this.getAvailability = getAvailability;
        }

        // GET: availability
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Get()
        {
            var result = await this.getAvailability.HandleAsync(new GetAvailabilityQuery());
            
            if (!result.IsOk())
            {
                return this.Request.CreateResponse(HttpStatusCode.ServiceUnavailable, result);
            }

            return this.Request.CreateResponse(result);
        }

        // GET: availability
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [AllowAnonymous]
        [AcceptVerbs("HEAD")]
        public Task<HttpResponseMessage> Head()
        {
            return this.Get();
        }
    }
}
