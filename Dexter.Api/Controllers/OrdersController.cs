using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dexter.Api.Controllers
{
    using Dexter.Api.Models;

    [RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            return this.Ok(Order.CreateOrders());
        }
    }
}
