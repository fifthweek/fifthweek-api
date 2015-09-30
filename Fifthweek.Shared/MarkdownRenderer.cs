namespace Fifthweek.Shared
{
    public class MarkdownRenderer : IMarkdownRenderer
    {
        public string GetHtml(string markdown)
        {
            return CommonMark.CommonMarkConverter.Convert(markdown);
        }
    }
}