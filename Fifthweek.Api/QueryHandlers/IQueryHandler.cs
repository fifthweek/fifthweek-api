namespace Fifthweek.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Queries;

    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}