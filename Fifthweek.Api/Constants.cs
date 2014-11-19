namespace Fifthweek.Api
{
    public static class Constants
    {
        public const string AdministratorUsers = "Admin";

        public const string FifthweekWebsiteOrigin = "http://localhost:9000";

        public const string DefaultAllowedOrigin = "*";
        
        public const string AllowedOriginHeaderKey = "Access-Control-Allow-Origin";

        public const string TokenAllowedOriginKey = "fifthweek:clientAllowedOrigin";

        public const string TokenRefreshTokenLifeTimeKey = "fifthweek:clientRefreshTokenLifeTime";

        public const string TokenClientIdKey = "fifthweek:client_id";
    }
}