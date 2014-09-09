namespace Dexter.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Queries;
    using Dexter.Api.Repositories;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, IdentityUser>
    {
        private readonly IAuthenticationRepository authenticationRepository;

        public GetUserQueryHandler(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        public Task<IdentityUser> HandleAsync(GetUserQuery query)
        {
            return this.authenticationRepository.FindInternalUserAsync(query.Username, query.Password);
        }
    }
}