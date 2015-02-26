namespace Fifthweek.Api.Persistence
{
    using System.Configuration;
    using System.Data.Common;
    using System.Data.SqlClient;

    public class FifthweekDbConnectionFactory : IFifthweekDbConnectionFactory
    {
        public const string DefaultConnectionStringKey = "FifthweekDbContext";
        public static readonly string DefaultConnectionString;

        private readonly string connectionString;

        static FifthweekDbConnectionFactory()
        {
            var configuration = ConfigurationManager.ConnectionStrings[DefaultConnectionStringKey];
            if (configuration != null)
            {
                DefaultConnectionString = configuration.ConnectionString;
            }
        }

        public FifthweekDbConnectionFactory()
            : this(DefaultConnectionString)
        {
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