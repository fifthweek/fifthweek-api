namespace Fifthweek.Api.Posts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ImagePostController : IImagePostController
    {
        private readonly ICommandHandler<PostImageCommand> postImage;
        private readonly ICommandHandler<ReviseImageCommand> reviseImage;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        public async Task PostImage(NewImageData imageData)
        {
            imageData.AssertBodyProvided("imageData");
            var image = imageData.Parse();

            var requester = await this.requesterContext.GetRequesterAsync();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postImage.HandleAsync(new PostImageCommand(
                requester,
                newPostId,
                image.CollectionId,
                image.FileId,
                image.Comment,
                image.ScheduledPostTime,
                image.IsQueued));
        }

        public async Task PutImage(string postId, RevisedImageData imageData)
        {
            postId.AssertUrlParameterProvided("postId");
            imageData.AssertBodyProvided("imageData");
            var image = imageData.Parse();
            var postIdObject = new PostId(postId.DecodeGuid());

            var requester = await this.requesterContext.GetRequesterAsync();

            await this.reviseImage.HandleAsync(new ReviseImageCommand(
                requester,
                postIdObject,
                image.ImageFileId,
                image.Comment));
        }
    }
}
