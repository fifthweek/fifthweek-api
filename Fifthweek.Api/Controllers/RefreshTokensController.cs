namespace Fifthweek.Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;
    using Fifthweek.Api.Repositories;

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