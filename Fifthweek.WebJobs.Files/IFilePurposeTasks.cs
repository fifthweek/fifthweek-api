namespace Fifthweek.WebJobs.Files
{
    using System.Collections.Generic;

    using Fifthweek.WebJobs.Files.Shared;

    public interface IFilePurposeTasks
    {
        IEnumerable<IFileTask> GetTasks(string purpose);
    }
}