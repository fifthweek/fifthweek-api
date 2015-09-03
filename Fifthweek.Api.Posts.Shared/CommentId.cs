namespace Fifthweek.Api.Posts.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class CommentId
    {
        public Guid Value { get; private set; }

        public static CommentId Random()
        {
            return new CommentId(Guid.NewGuid());
        }
    }
}