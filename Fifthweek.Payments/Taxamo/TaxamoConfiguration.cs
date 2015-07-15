namespace Fifthweek.Payments.Taxamo
{
    using System;

    public static class TaxamoConfiguration
    {
        public const string TaxamoLiveApiKeyEnvironmentVariable = "TaxamoLiveApiKey";
        public const string TaxamoTestApiKeyEnvironmentVariable = "TaxamoTestApiKey";

        public static string GetTestApiKey()
        {
            var apiKey = TryGetApiKey(TaxamoTestApiKeyEnvironmentVariable);

            if (apiKey == null)
            {
                throw new Exception("Environment variable '" + TaxamoTestApiKeyEnvironmentVariable + "' not set.");
            }

            return apiKey;
        }

        public static string GetLiveApiKey()
        {
            var apiKey = TryGetApiKey(TaxamoLiveApiKeyEnvironmentVariable);

            if (apiKey == null)
            {
                throw new Exception("Environment variable '" + TaxamoLiveApiKeyEnvironmentVariable + "' not set. This should be set to the test key on the developer machine.");
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