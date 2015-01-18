namespace Fifthweek.Api.Collections
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CollectionId
    {
        public Guid Value { get; private set; }
    }
}