namespace Fifthweek.Api.Posts.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class PostId
    {
        public Guid Value { get; private set; }
    }
}