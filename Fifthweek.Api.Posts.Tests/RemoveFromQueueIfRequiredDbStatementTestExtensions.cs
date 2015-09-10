namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Posts.Shared;

    using Moq;

    public static class RemoveFromQueueIfRequiredDbStatementTestExtensions
    {
        public static void SetupFor(this Mock<IDefragmentQueueIfRequiredDbStatement> self, PostId postId)
        {
            self.Setup(_ => _.ExecuteAsync(postId, It.Is<DateTime>(now => now.Kind == DateTimeKind.Utc), It.IsAny<Func<Task>>()))
                .Callback((PostId id, DateTime now, Func<Task> potentialRemovalOperation) => potentialRemovalOperation().Wait())
                .Returns(Task.FromResult(0));
        }
    }
}