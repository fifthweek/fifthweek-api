namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Migrations;

    [AutoConstructor]
    public partial class TestDatabase
    {
        private readonly string connectionString;

        public static async Task<TestDatabase> CreateNewAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            var connectionString = NewLocalDbConnectionString();
            var migrationConfiguration = new Configuration()
            {
                TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient")
            };

            new DbMigrator(migrationConfiguration).Update();

            var secondsElapsed = Math.Round(stopwatch.Elapsed.TotalSeconds, 2);
            Trace.WriteLine(string.Format("Migrated database in {0}s", secondsElapsed));

            var seed = new TemporaryDatabaseSeed(() => new FifthweekDbContext(connectionString));
            await seed.PopulateWithDummyEntitiesAsync();

            return new TestDatabase(connectionString);
        }

        public IFifthweekDbContext NewDatabaseContext()
        {
            return new FifthweekDbContext(this.connectionString);
        }

        private static string NewLocalDbConnectionString()
        {
            const string FileName = "Fifthweek_Test.mdf";
            var filePath = System.IO.Path.GetTempPath() + "\\" + FileName;
            return string.Format("Data Source=(LocalDb)\\v11.0;AttachDbFilename={0};Integrated Security=SSPI;", filePath);
        }
    }
}
