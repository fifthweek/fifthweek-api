namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.Common;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Migrations;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class TestDatabase : IFifthweekDbConnectionFactory
    {
        // Increment this when changing database seed state.
        private const int SeedStateVersion = 34;

        private static bool migrationRequired = true;

        private readonly FifthweekDbConnectionFactory connectionFactory;

        public static async Task<TestDatabase> CreateNewAsync()
        {
            var migrateDatabase = migrationRequired;
            if (migrateDatabase)
            {
                migrationRequired = false;
            }

            var connectionString = NewLocalDbConnectionString();

            if (migrateDatabase)
            {
                var stopwatch = Stopwatch.StartNew();
                var migrationConfiguration = new Configuration()
                {
                    TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient")
                };

                new DbMigrator(migrationConfiguration).Update();

                Trace.WriteLine(string.Format("Migrated database in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));
            }

            var seed = new TestDatabaseSeed(() => new FifthweekDbContext(connectionString));

            if (migrateDatabase)
            {
                await seed.PopulateWithDummyEntitiesAsync();
            }
            else
            {
                await seed.AssertSeedStateUnchangedAsync();
            }

            return new TestDatabase(new FifthweekDbConnectionFactory(connectionString));
        }

        public DbConnection CreateConnection()
        {
            return this.connectionFactory.CreateConnection();
        }

        public FifthweekDbContext CreateContext()
        {
            return this.connectionFactory.CreateContext();
        }

        private static string NewLocalDbConnectionString()
        {
            var fileName = string.Format("Fifthweek_Test_{0}.mdf", SeedStateVersion);
            var filePath = System.IO.Path.GetTempPath() + "\\" + fileName;
            return string.Format("Data Source=(LocalDb)\\v11.0;AttachDbFilename={0};Integrated Security=SSPI;", filePath);
        }
    }
}
