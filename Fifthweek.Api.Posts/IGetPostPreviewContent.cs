namespace Fifthweek.Api.Posts
{
    using Fifthweek.Api.Posts.Shared;

    public interface IGetPostPreviewContent
    {
        string Execute(string postContent, PreviewText previewText);
    }
}