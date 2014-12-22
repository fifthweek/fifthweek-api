namespace Fifthweek.Api.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    using Fifthweek.Api.Models;

    using Newtonsoft.Json;

    public class SlackReportingService : IReportingService
    {
        public async Task ReportErrorAsync(Exception exception, string identifier, Developer developer)
        {
            var client = new HttpClient();
            var uri = "https://hooks.slack.com/services/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";

            var sb = new StringBuilder();
            if (exception is ExternalErrorException)
            {
                sb.AppendLine("An error was reported via the Fifthweek API:");
                sb.Append("```");
                sb.Append(TruncateError(exception.Message));
                sb.Append("```");
            }
            else
            {
                sb.AppendLine("An exception occured:");
                sb.Append("```");
                sb.Append(TruncateError(exception.ToString()));
                sb.Append("```");
            }

            var content = new SlackContent(
                sb.ToString().Replace("\r", string.Empty),
                developer == null ? null : "@" + developer.SlackName);
            
            var response = await client.PostAsync(uri, content, new JsonMediaTypeFormatter());

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Failed to post to slack. Status Code " + response.StatusCode + ", Message: " + responseContent);
            }
        }

        private static string TruncateError(string errorString)
        {
            const int MaxLength = 4000;
            if (errorString.Length > MaxLength)
            {
                errorString = errorString.Substring(0, MaxLength) + " [Truncated]";
            }

            return errorString;
        }

        private class SlackContent
        {
            public SlackContent(string text, string channel)
            {
                this.Text = text;
                this.Channel = channel;
            }

            [JsonProperty("text")]
            public string Text { get; private set; }

            [JsonProperty("channel", DefaultValueHandling = DefaultValueHandling.Ignore)]
            public string Channel { get; private set; }
        }
    }
}