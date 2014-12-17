namespace Fifthweek.Api.Services
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    public class SlackReportingService : IReportingService
    {
        public async Task ReportErrorAsync(Exception exception, string identifier)
        {
            var client = new HttpClient();
            var uri = "https://hooks.slack.com/services/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";

            var sb = new StringBuilder();
            if (exception is ExternalErrorException)
            {
                sb.AppendLine("An error was reported via the Fifthweek API:");
                sb.Append("```");
                sb.Append(exception.Message);
                sb.Append("```");
            }
            else
            {
                sb.AppendLine("An exception occured:");
                sb.Append("```");
                sb.Append(exception.ToString());
                sb.Append("```");
            }

            var content = new SlackContent(sb.ToString().Replace("\r", string.Empty));
            
            var response = await client.PostAsync(uri, content, new JsonMediaTypeFormatter());

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception("Failed to post to slack. Status Code " + response.StatusCode + ", Message: " + responseContent);
            }
        }

        private class SlackContent
        {
            public SlackContent(string text)
            {
                this.Text = text;
            }

            [JsonProperty("text")]
            public string Text { get; private set; }
        }
    }
}