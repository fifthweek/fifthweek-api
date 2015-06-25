namespace Fifthweek.Payments.Shared
{
    using System.Threading.Tasks;

    public interface IKeepAliveHandler
    {
        Task KeepAliveAsync();
    }
}