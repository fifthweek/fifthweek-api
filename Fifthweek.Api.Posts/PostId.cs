namespace Fifthweek.Api.Posts
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class PostId
    {
        public Guid Value { get; private set; }
    }
}