namespace Fifthweek.Payments.Stripe
{
    using System;

    public static class StripeConfiguration
    {
        public const string StripeLiveApiKeyEnvironmentVariable = "StripeLiveApiKey";
        public const string StripeTestApiKeyEnvironmentVariable = "StripeTestApiKey";

        public static string GetTestApiKey()
        {
            var apiKey = TryGetApiKey(StripeTestApiKeyEnvironmentVariable);

            if (apiKey == null)
            {
                throw new Exception("Environment variable '" + StripeTestApiKeyEnvironmentVariable + "' not set.");
            }

            return apiKey;
        }

        public static string GetLiveApiKey()
        {
            var apiKey = TryGetApiKey(StripeLiveApiKeyEnvironmentVariable);

            if (apiKey == null)
            {
                throw new Exception("Environment variable '" + StripeLiveApiKeyEnvironmentVariable + "' not set. This should only be set for the live website.");
            }

            return apiKey;
        }

        private static string TryGetApiKey(string environmentVariable)
        {
            var apiKey
                = Environment.GetEnvironmentVariable(environmentVariable, EnvironmentVariableTarget.Process)
                  ?? Environment.GetEnvironmentVariable(environmentVariable, EnvironmentVariableTarget.User)
                  ?? Environment.GetEnvironmentVariable(environmentVariable, EnvironmentVariableTarget.Machine);

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return null;
            }

            return apiKey;
        }
    }
}