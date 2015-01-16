namespace Fifthweek.WebJobs.Files
{
    using System.Collections.Generic;

    using Fifthweek.WebJobs.Files.Shared;

    public interface IFilePurposeToTasksMappings
    {
        IEnumerable<IFileTask> GetTasks(string purpose);
    }
}