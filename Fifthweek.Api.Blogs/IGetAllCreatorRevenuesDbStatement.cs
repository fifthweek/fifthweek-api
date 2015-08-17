namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGetAllCreatorRevenuesDbStatement
    {
        Task<GetAllCreatorRevenuesResult> ExecuteAsync(DateTime releasableRevenueDate);
    }
}