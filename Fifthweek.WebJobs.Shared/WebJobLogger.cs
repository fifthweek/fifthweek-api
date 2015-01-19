namespace Fifthweek.WebJobs.Shared
{
    using System.IO;
    using System.Threading.Tasks;

    public class WebJobLogger : ILogger
    {
        private readonly TextWriter textWriter;

        public WebJobLogger(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        public void Info(string message, params object[] args)
        {
            this.textWriter.WriteLine("Info: " + message, args);
        }

        public void Warn(string message, params object[] args)
        {
            this.textWriter.WriteLine("Warn: " + message, args);
        }

        public void Error(string message, params object[] args)
        {
            this.textWriter.WriteLine("Error: " + message, args);
        }
    }
}