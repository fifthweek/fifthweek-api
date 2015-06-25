namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetAllSubscribersDbStatement
    {
        Task<IReadOnlyList<UserId>> ExecuteAsync();
    }
}