namespace Fifthweek.Azure
{
    using System.Threading.Tasks;

    public interface IKeepAliveHandler
    {
        Task KeepAliveAsync();
    }
}