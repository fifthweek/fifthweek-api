namespace Fifthweek.Api.FileManagement
{
    using System;

    using Fifthweek.Api.Core;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class FileId
    {
        public Guid Value { get; private set; }
    }
}