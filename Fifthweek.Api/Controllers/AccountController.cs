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

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAuthenticationRepository authenticationRepository;

        private readonly IClientRepository clientRepository;

        private readonly ICommandHandler<RegisterInternalUserCommand> registerInternalUser;

        public AccountController(
            IAuthenticationRepository authenticationRepository,
            IClientRepository clientRepository,
            ICommandHandler<RegisterInternalUserCommand> registerInternalUser)
        {
            this.authenticationRepository = authenticationRepository;
            this.clientRepository = clientRepository;
            this.registerInternalUser = registerInternalUser;
        }

        // POST api/Account/RegisterInternalUser
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [ValidateModel]
        [AllowAnonymous]
        [Route("RegisterInternalUser")]
        public async Task<IHttpActionResult> RegisterInternalUser(InternalRegistrationData registrationData)
        {
            await this.registerInternalUser.HandleAsync(new RegisterInternalUserCommand(registrationData));
            return this.Ok();
        }
    }
}
