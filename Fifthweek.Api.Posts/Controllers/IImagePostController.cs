namespace Fifthweek.Api.Posts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    public interface IImagePostController
    {
        Task PostImage(NewImageData imageData);

        Task PutImage(string postId, RevisedImageData imageData);
    }
}