namespace Fifthweek.Api.Availability
{
    public class AvailabilityResult
    {
        public AvailabilityResult(bool api, bool database)
        {
            this.Api = api;
            this.Database = database;
        }

        public bool Database { get; private set; }

        public bool Api { get; private set; }

        public bool IsOk()
        {
            return this.Api && this.Database;
        }
    }
}