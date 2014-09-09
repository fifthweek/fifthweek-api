namespace Dexter.Api.Models
{
    using Newtonsoft.Json;

    public class ParsedExternalAccessToken
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("app_id")]
        public string ApplicationId { get; set; }
    }
}