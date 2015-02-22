namespace Fifthweek.WebJobs.Shared
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class WebJobLogger : ILogger
    {
        private readonly string id = Guid.NewGuid().ToString().Substring(0, 8);

        private readonly TextWriter textWriter;

        public WebJobLogger(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        public void Info(string message, params object[] args)
        {
            this.Log(string.Format("Info: " + message, args));
        }

        public void Warn(string message, params object[] args)
        {
            this.Log(string.Format("Warn: " + message, args));
        }

        public void Error(string message, params object[] args)
        {
            this.Log(string.Format("Error: " + message, args));
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