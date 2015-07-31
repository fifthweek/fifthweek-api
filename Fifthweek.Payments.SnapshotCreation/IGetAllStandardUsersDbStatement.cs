namespace Fifthweek.Payments.SnapshotCreation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetAllStandardUsersDbStatement
    {
        Task<IReadOnlyList<UserId>> ExecuteAsync();
    }
}