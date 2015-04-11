namespace Fifthweek.Api.Blogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IUpdateFreeAccessUsersDbStatement
    {
        Task ExecuteAsync(BlogId blogId, IReadOnlyList<ValidEmail> emails);
    }
}