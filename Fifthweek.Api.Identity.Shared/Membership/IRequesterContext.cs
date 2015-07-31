namespace Fifthweek.Api.Identity.Shared.Membership
{
    using System.Threading.Tasks;

    public interface IRequesterContext
    {
        Task<Requester> GetRequesterAsync();
    }
}