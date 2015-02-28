namespace Fifthweek.Api.Posts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    public interface INotePostController
    {
        Task PostNote(NewNoteData noteData);

        Task PutNote(string postId, RevisedNoteData noteData);
    }
}