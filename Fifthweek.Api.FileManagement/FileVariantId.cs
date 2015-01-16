namespace Fifthweek.Api.FileManagement
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class FileVariantId
    {
        public Guid Value { get; private set; }
    }
}