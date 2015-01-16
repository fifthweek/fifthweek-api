namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Migrations;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class TestDatabase
    {
        private static bool migrationRequired = true;

        private readonly string connectionString;

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

            return new TestDatabase(connectionString);
        }

        public FifthweekDbContext NewDatabaseContext()
        {
            return new FifthweekDbContext(this.connectionString);
        }

        private static string NewLocalDbConnectionString()
        {
            const string FileName = "Fifthweek_Test_3.mdf";
            var filePath = System.IO.Path.GetTempPath() + "\\" + FileName;
            return string.Format("Data Source=(LocalDb)\\v11.0;AttachDbFilename={0};Integrated Security=SSPI;", filePath);
        }
    }
}
