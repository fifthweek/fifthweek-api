namespace Dexter.Api
{
    public static class Constants
    {
        public const string AdministratorUsers = "Admin";

        public const string DexterWebsiteOrigin = "http://localhost:9000";

        public const string DefaultAllowedOrigin = "*";
        
        public const string AllowedOriginHeaderKey = "Access-Control-Allow-Origin";

        public const string TokenAllowedOriginKey = "dexter:clientAllowedOrigin";

        public const string TokenRefreshTokenLifeTimeKey = "dexter:clientRefreshTokenLifeTime";

        public const string TokenClientIdKey = "dexter:client_id";
    }
}