namespace Fifthweek.Api.FileManagement
{
    using System.Collections.Generic;

    using Fifthweek.Api.FileManagement.FileTasks;

    public interface IFilePurposeTasks
    {
        IEnumerable<IFileTask> GetTasks(string purpose);
    }
}