namespace Dexter.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Queries;

    public class NullQueryHandler : IQueryHandler<NullQuery, bool>
    {
        public Task<bool> HandleAsync(NullQuery query)
        {
            return Task.FromResult<bool>(false);
        }
    }
}