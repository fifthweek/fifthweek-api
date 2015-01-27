namespace Fifthweek.Api.FileManagement.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class FileId
    {
        public Guid Value { get; private set; }
    }
}