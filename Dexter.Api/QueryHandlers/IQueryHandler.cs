namespace Dexter.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Queries;

    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}