namespace Fifthweek.Api.Core
{
    using System.Net.Mail;

    public class Constants
    {
        public const string DefaultAllowedOrigin = "*";

        public const string AllowedOriginHeaderKey = "Access-Control-Allow-Origin";

        public const string TokenAllowedOriginKey = "fifthweek:clientAllowedOrigin";

        public const string TokenRefreshTokenLifeTimeKey = "fifthweek:clientRefreshTokenLifeTime";

        public const string TokenClientIdKey = "fifthweek:client_id";

        public const string ErrorEmailAddress = "services@fifthweek.com";

        public static readonly MailAddress FifthweekEmailAddress = new MailAddress("hello@fifthweek.com", "Fifthweek");

        public static readonly string FifthweekWebsiteOriginRegex = GetWebsiteOriginRegex();
        public static readonly string FifthweekWebsiteOriginDefault = GetWebsiteOriginDefault();

        private static string GetWebsiteOriginDefault()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ACCESS_CONTROL_ALLOW_ORIGIN_DEFAULT"];
        }

        private static string GetWebsiteOriginRegex()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ACCESS_CONTROL_ALLOW_ORIGIN_REGEX"];
        }
    }
}