namespace Fifthweek.Shared
{
    public class BadRequestException : RecoverableException
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}