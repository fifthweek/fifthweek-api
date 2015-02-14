namespace Fifthweek.Shared
{
    public interface IHtmlLinter
    {
        /// <remarks>
        /// Safe for HTML emails only. May have adverse effects on more complicated pages including JavaScript.
        /// </remarks>
        string RemoveWhitespaceForHtmlEmail(string html);
    }
}