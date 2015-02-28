namespace Fifthweek.Api.Posts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    public interface IFilePostController
    {
        [Route("files")]
        Task PostFile([FromBody]NewFileData fileData);

        [Route("files/{postId}")]
        Task PutFile(string postId, [FromBody]RevisedFileData fileData);
    }
}