using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fifthweek.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;
    using Fifthweek.Api.Repositories;
    using Fifthweek.Api.Results;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    using Newtonsoft.Json.Linq;

    [RoutePrefix("Account")]
    public class AccountController : ApiController
    {
        private readonly ICommandHandler<RegisterUserCommand> registerUser;

        public AccountController(
            ICommandHandler<RegisterUserCommand> registerUser)
        {
            this.registerUser = registerUser;
        }

        // POST api/Account/RegisterUser
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [ValidateModel]
        [AllowAnonymous]
        [Route("RegisterUser")]
        public async Task<IHttpActionResult> RegisterUser(RegistrationData registrationData)
        {
            await this.registerUser.HandleAsync(new RegisterUserCommand(registrationData));
            return this.Ok();
        }
    }
}
