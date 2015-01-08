using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using Fifthweek.Api.Core;
using Fifthweek.Api.Persistence.Identity;
using Fifthweek.Api.Persistence.Migrations;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    [AutoConstructor]
    public partial class TemporaryDatabase : IDisposable
    {
        private readonly string connectionString;

        public void Dispose()
        {
            var connection = this.NewDbContext().Database.Connection;
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

        public IFifthweekDbContext NewDbContext()
        {
            return new FifthweekDbContext(this.connectionString);
        }

        public static TemporaryDatabase CreateNew()
        {
            var connectionString = NewUniqueLocalDbConnectionString();

            var migrationConfiguration = new Configuration
            {
                TargetDatabase = new DbConnectionInfo(connectionString, "System.Data.SqlClient")
            };
            new DbMigrator(migrationConfiguration).Update();
            
            using (var db = new FifthweekDbContext(connectionString))
            {
                // Trigger any migration errors early.
                Trace.WriteLine("Starting with " + db.Users.Count() + " users.");
            }

            return new TemporaryDatabase(connectionString);
        }

        private static string NewUniqueLocalDbConnectionString()
        {
            var fileName = string.Format("Fifthweek_Test_{0}.mdf", Guid.NewGuid());
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName;
            return string.Format("Data Source=(LocalDb)\\v11.0;AttachDbFilename={0};Integrated Security=SSPI;", filePath);
        }
    }
}
