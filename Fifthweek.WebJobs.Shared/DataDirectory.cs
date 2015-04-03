namespace Fifthweek.WebJobs.Shared
{
    using System;
    using System.Configuration;
    using System.IO;

    public static class DataDirectory
    {
        public static void ConfigureDataDirectory()
        {
            var dataDirectory = ConfigurationManager.AppSettings["DataDirectory"];
            var absoluteDataDirectory = Path.GetFullPath(dataDirectory);
            AppDomain.CurrentDomain.SetData("DataDirectory", absoluteDataDirectory);
        }
    }
}