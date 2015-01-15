namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Migrations;

    [AutoConstructor]
    public partial class TemporaryDatabase : IDisposable
    {
        private readonly string connectionString;
        private readonly Task databaseInitialization;

        public static TemporaryDatabase CreateNew()
        {
            var stopwatch = Stopwatch.StartNew();

            var connectionString = NewUniqueLocalDbConnectionString();
            var migrationConfiguration = new Configuration()
            {
                TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient")
            };

            new DbMigrator(migrationConfiguration).Update();

            var secondsElapsed = Math.Round(stopwatch.Elapsed.TotalSeconds, 2);
            Trace.WriteLine(string.Format("Migrated database in {0}s", secondsElapsed));
            
            var seed = new TemporaryDatabaseSeed(() => new FifthweekDbContext(connectionString));
            var seedTask = seed.PopulateWithDummyEntitiesAsync();

            return new TemporaryDatabase(connectionString, seedTask);
        }

        public IFifthweekDbContext NewDatabaseContext()
        {
            return new FifthweekDbContext(this.connectionString);
        }

        public Task ReadyAsync()
        {
            return this.databaseInitialization;
        }

        public void Dispose()
        {
            var stopwatch = Stopwatch.StartNew();

            this.Drop();

            var secondsElapsed = Math.Round(stopwatch.Elapsed.TotalSeconds, 2);
            Trace.WriteLine(string.Format("Dropped database in {0}s", secondsElapsed));
        }

        private static string NewUniqueLocalDbConnectionString()
        {
            var fileName = string.Format("Fifthweek_Test_{0}.mdf", Guid.NewGuid());
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName;
            return string.Format("Data Source=(LocalDb)\\v11.0;AttachDbFilename={0};Integrated Security=SSPI;", filePath);
        }

        private void Drop()
        {
            var connection = this.NewDatabaseContext().Database.Connection;
            connection.Open();
            try
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = string.Format(@"
                    USE MASTER;
                    ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE [{0}];
                    ", connection.Database);

                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
