namespace Fifthweek.Api.Collections.Controllers
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class NewQueueData
    {
        public NewQueueData()
        {
        }

        public BlogId BlogId { get; set; }

        [Parsed(typeof(ValidQueueName))]
        public string Name { get; set; }
    }
}