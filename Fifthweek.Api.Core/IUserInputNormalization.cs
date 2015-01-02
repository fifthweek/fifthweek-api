namespace Fifthweek.Api.Core
{
    /// <summary>
    /// Normalisation occurs before validation and is intended to be fast and synchronous in execution.
    /// </summary>
    public interface IUserInputNormalization
    {
        string NormalizeEmailAddress(string emailAddress);

        string NormalizeUsername(string username);
    }
}