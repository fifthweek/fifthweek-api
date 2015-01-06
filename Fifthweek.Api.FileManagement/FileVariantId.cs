namespace Fifthweek.Api.FileManagement
{
    using System;

    using Fifthweek.Api.Core;

    public class FileVariantId : IdBase<Guid>
    {
        public FileVariantId(Guid id)
            : base(id)
        {
        }
    }
}