namespace Dexter.Api.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {

        private AuthenticationRepository repository = null;

        public RefreshTokensController()
        {
            this.repository = new AuthenticationRepository();
        }

        [RequireHttps]
        [Authorize(Users = Constants.AdministratorUsers)]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(this.repository.GetAllRefreshTokens());
        }

        [RequireHttps]
        [Authorize(Users = Constants.AdministratorUsers)]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await this.repository.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

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