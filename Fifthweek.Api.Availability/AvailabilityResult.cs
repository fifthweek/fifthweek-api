namespace Fifthweek.Api.Availability
{
    public class AvailabilityResult
    {
        public AvailabilityResult(bool api, bool database, bool payments)
        {
            this.Api = api;
            this.Database = database;
            this.Payments = payments;
        }

        public bool Database { get; private set; }

        public bool Api { get; private set; }

        public bool Payments { get; private set; }

        public bool IsOk()
        {
            return this.Api && this.Database && this.Payments;
        }
    }
}