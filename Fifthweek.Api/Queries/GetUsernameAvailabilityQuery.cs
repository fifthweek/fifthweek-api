namespace Fifthweek.Api.Queries
{
    public class GetUsernameAvailabilityQuery : IQuery<bool>
    {
        public GetUsernameAvailabilityQuery(string username)
        {
            this.Username = username;
        }

        public string Username { get; private set; }
    }
}