namespace Fifthweek.Api.Posts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("posts/files"), AutoConstructor]
    public partial class FilePostController : ApiController
    {
        private readonly ICommandHandler<PostFileCommand> postFile;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        public async Task<IHttpActionResult> PostFile(NewFileData fileData)
        {
            fileData.AssertBodyProvided("fileData");
            var file = fileData.Parse();

            var requester = this.requesterContext.GetRequester();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postFile.HandleAsync(new PostFileCommand(
                requester,
                newPostId,
                file.CollectionId,
                file.FileId,
                file.Comment,
                file.ScheduledPostDate,
                file.IsQueued));

            return this.Ok();
        }
    }
}
