namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    public interface IUpdateCollectionFieldsDbStatement
    {
        Task ExecuteAsync(Collection collection);
    }
}