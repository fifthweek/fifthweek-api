namespace Fifthweek.WebJobs.Shared
{
    using System;
    using System.Threading.Tasks;

    public interface ILogger
    {
        void Info(string message, params object[] args);

        void Warn(string message, params object[] args);

        void Error(Exception exception);
    }
}