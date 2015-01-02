namespace Fifthweek.Api
{
    using Fifthweek.Api.Core;

    public class UserInputNormalization : IUserInputNormalization
    {
        public string NormalizeEmailAddress(string emailAddress)
        {
            return NoWhitespaceLowerCase(emailAddress);
        }

        public string NormalizeUsername(string username)
        {
            return NoWhitespaceLowerCase(username);
        }

        private static string NoWhitespaceLowerCase(string value)
        {
            return value.Trim().ToLower();
        }
    }
}