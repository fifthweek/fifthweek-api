namespace Fifthweek.Api.Blogs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;

    public interface IGetFreeAccessUsersDbStatement
    {
        Task<GetFreeAccessUsersResult> ExecuteAsync(BlogId blogId);
    }
}