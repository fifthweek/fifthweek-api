namespace Fifthweek.Azure
{
    using System;

    public static class AzureConfiguration
    {
        public const string AzureStorageConnectionStringEnvironmentVariable = "StorageConnectionString";
        
        public static string GetStorageConnectionString()
        {
            var storageConnectionString 
                = Environment.GetEnvironmentVariable(AzureStorageConnectionStringEnvironmentVariable, EnvironmentVariableTarget.Process)
                ?? Environment.GetEnvironmentVariable(AzureStorageConnectionStringEnvironmentVariable, EnvironmentVariableTarget.User)
                ?? Environment.GetEnvironmentVariable(AzureStorageConnectionStringEnvironmentVariable, EnvironmentVariableTarget.Machine);

            if (string.IsNullOrWhiteSpace(storageConnectionString))
            {
                throw new Exception(
                    "Environment variable '" + AzureStorageConnectionStringEnvironmentVariable + "' not set.");
            }

            return storageConnectionString;
        }
    }
}