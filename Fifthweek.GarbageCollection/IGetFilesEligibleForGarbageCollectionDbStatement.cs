namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGetFilesEligibleForGarbageCollectionDbStatement
    {
        Task<IReadOnlyList<OrphanedFileData>> ExecuteAsync(DateTime endDateExclusive);
    }
}