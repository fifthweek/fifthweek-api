namespace Fifthweek.Api.Persistence
{
    using System.Configuration;
    using System.Data.Common;
    using System.Data.SqlClient;

    public class FifthweekDbConnectionFactory : IFifthweekDbConnectionFactory
    {
        public const string DefaultConnectionString = "FifthweekDbContext";

        private readonly string connectionString;

        public FifthweekDbConnectionFactory()
            : this(DefaultConnectionString)
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[DefaultConnectionString].ConnectionString;
        }

        public FifthweekDbConnectionFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DbConnection CreateConnection()
        {
            return new SqlConnection(this.connectionString);
        }

        public FifthweekDbContext CreateContext()
        {
            return new FifthweekDbContext(this.connectionString);
        }
    }
}