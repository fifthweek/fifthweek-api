namespace Fifthweek.WebJobs.Shared
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Fifthweek.Logging;
    using Fifthweek.Shared;

    public class WebJobLogger : ILogger
    {
        private static readonly IErrorReportingService ReportingService = HardwiredDependencies.NewDefaultReportingService();

        private readonly string id = Guid.NewGuid().ToString().Substring(0, 8);

        private readonly TextWriter textWriter;

        private readonly string webJobIdentifier;

        public WebJobLogger(TextWriter textWriter, string webJobIdentifier)
        {
            this.textWriter = textWriter;
            this.webJobIdentifier = webJobIdentifier;
        }

        public void Info(string message, params object[] args)
        {
            this.Log(string.Format("Info: " + message, args));
        }

        public void Warn(string message, params object[] args)
        {
            var formattedMessage = string.Format("Warn: " + message, args);
            this.Log(formattedMessage);
            this.ReportException(new WarningException(formattedMessage));
        }

        public void Error(Exception exception)
        {
            this.Log(string.Format("Error: " + exception));
            this.ReportException(exception);
        }

        private async void ReportException(Exception exception)
        {
            try
            {
                await ReportingService.ReportErrorAsync(exception, this.webJobIdentifier, null);
            }
            catch (Exception t)
            {
                this.Log(string.Format("Error: Failed to report error: " + t));
            }
        }

        private void Log(string output)
        {
            this.textWriter.WriteLine(output);
            Console.WriteLine(this.id + " " + this.GetNowString() + " " + output);
        }

        private string GetNowString()
        {
            return DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss.fff");
        }
    }
}