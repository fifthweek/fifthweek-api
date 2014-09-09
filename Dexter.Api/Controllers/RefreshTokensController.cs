namespace Dexter.Api.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Dexter.Api.Repositories;

    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {
        private readonly AuthenticationRepository repository;

        public RefreshTokensController()
        {
            this.repository = new AuthenticationRepository();
        }

        [RequireHttps]
        [Authorize(Users = Constants.AdministratorUsers)]
        [Route("")]
        public IHttpActionResult Get()
        {
            return this.Ok(this.repository.GetAllRefreshTokens());
        }

        [RequireHttps]
        [Authorize(Users = Constants.AdministratorUsers)]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await this.repository.RemoveRefreshToken(tokenId);
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
                this.repository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}