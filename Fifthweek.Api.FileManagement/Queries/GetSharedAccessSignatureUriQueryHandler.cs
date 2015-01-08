namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class GetSharedAccessSignatureUriQueryHandler : IQueryHandler<GetSharedAccessSignatureUriQuery, string>
    {
        public Task<string> HandleAsync(GetSharedAccessSignatureUriQuery query)
        {
            return Task.FromResult("uri");
        }
    }
}