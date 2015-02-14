namespace Fifthweek.Shared
{
    using System.Text.RegularExpressions;

    public class HtmlLinter : IHtmlLinter
    {
        private static readonly Regex BetweenTags = new Regex(@">\s+<", RegexOptions.Compiled);
        private static readonly Regex LineBreaks = new Regex(@"\n", RegexOptions.Compiled);

        /// <remarks>
        /// Safe for HTML emails only. May have adverse effects on more complicated pages including JavaScript.
        /// </remarks>
        public string RemoveWhitespaceForHtmlEmail(string html)
        {
            html = LineBreaks.Replace(html, " ");
            html = BetweenTags.Replace(html, "> <");
            html = html.Trim();
            return html;
        }
    }
}