namespace Fifthweek.Api.Blogs.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class BlogId
    {
        public Guid Value { get; private set; }

        public static BlogId Random()
        {
            return new BlogId(Guid.NewGuid());
        }
    }
}