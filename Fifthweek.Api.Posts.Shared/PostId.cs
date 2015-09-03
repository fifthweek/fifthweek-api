namespace Fifthweek.Api.Posts.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class PostId
    {
        public Guid Value { get; private set; }

        public static PostId Random()
        {
            return new PostId(Guid.NewGuid());
        }
    }
}