namespace Fifthweek.Webjobs.Files
{
    using System.Collections.Generic;

    using Fifthweek.Webjobs.Files.Shared;

    public interface IFilePurposeToTasksMappings
    {
        IEnumerable<IFileTask> GetTasks(string purpose);
    }
}