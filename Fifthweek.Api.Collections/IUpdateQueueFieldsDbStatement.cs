namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    public interface IUpdateQueueFieldsDbStatement
    {
        Task ExecuteAsync(Queue queue);
    }
}