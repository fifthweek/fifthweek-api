namespace Dexter.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Dexter.Api.CommandHandlers;
    using Dexter.Api.Commands;
    using Dexter.Api.Entities;
    using Dexter.Api.Models;
    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;

    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {
        private readonly ICommandHandler<RemoveRefreshTokenCommand> removeRefreshToken;

        private readonly IQueryHandler<GetAllRefreshTokensQuery, List<RefreshToken>> getAllRefreshTokens;

        public RefreshTokensController(
            ICommandHandler<RemoveRefreshTokenCommand> removeRefreshToken,
            IQueryHandler<GetAllRefreshTokensQuery, List<RefreshToken>> getAllRefreshTokens)
        {
            this.removeRefreshToken = removeRefreshToken;
            this.getAllRefreshTokens = getAllRefreshTokens;
        }

        [RequireHttps]
        [ConvertExceptionsToResponses]
        [Authorize(Users = Constants.AdministratorUsers)]
        [Route("")]
        public IHttpActionResult Get()
        {
            var result = this.getAllRefreshTokens.HandleAsync(GetAllRefreshTokensQuery.Default);
            return this.Ok(result);
        }

        [RequireHttps]
        [ConvertExceptionsToResponses]
        [Authorize(Users = Constants.AdministratorUsers)]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            await this.removeRefreshToken.HandleAsync(new RemoveRefreshTokenCommand(new HashedRefreshTokenId(tokenId)));
            return this.Ok();
        }
    }
}