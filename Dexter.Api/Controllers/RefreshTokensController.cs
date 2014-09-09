namespace Dexter.Api.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Dexter.Api.Repositories;

    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {
        private readonly IAuthenticationRepository authenticationRepository;

        public RefreshTokensController(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        [RequireHttps]
        [Authorize(Users = Constants.AdministratorUsers)]
        [Route("")]
        public IHttpActionResult Get()
        {
            return this.Ok(this.authenticationRepository.GetAllRefreshTokens());
        }

        [RequireHttps]
        [Authorize(Users = Constants.AdministratorUsers)]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await this.authenticationRepository.RemoveRefreshToken(tokenId);
            if (result)
            {
                return this.Ok();
            }

            return this.BadRequest("Token Id does not exist");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.authenticationRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}