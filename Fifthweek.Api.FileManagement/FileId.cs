namespace Fifthweek.Api.FileManagement
{
    using System;

    using Fifthweek.Api.Core;

    public class FileId : IdBase<Guid>
    {
        public FileId(Guid id)
            : base(id)
        {
        }
    }
}