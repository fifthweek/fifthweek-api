namespace Fifthweek.Azure
{
    using System;

    public static class AzureConfiguration
    {
        public const string AzureStorageConnectionStringEnvironmentVariable = "StorageConnectionString";
        
        public static string GetStorageConnectionString()
        {
            var storageConnectionString = Environment.GetEnvironmentVariable(AzureStorageConnectionStringEnvironmentVariable);
            return storageConnectionString;
        }
    }
}