namespace Fifthweek.Api.Core
{
    public class BadRequestException : RecoverableException
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}