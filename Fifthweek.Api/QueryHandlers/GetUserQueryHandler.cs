namespace Fifthweek.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Queries;
    using Fifthweek.Api.Repositories;

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