namespace Fifthweek.Api.Logging
{
    using Newtonsoft.Json.Linq;

    public class BrowserLogMessage
    {
        public string Level { get; set; }

        public JToken Payload { get; set; }
    }
}