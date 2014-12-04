namespace Fifthweek.Api
{
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Http;

    using Fifthweek.Api.Migrations;
    using Fifthweek.Api.Repositories;

    public static class DatabaseConfig
    {
        public static void Register()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FifthweekDbContext, Configuration>());
            
            // This is to ensure the database is created on startup, before any commands occur that are wrapped in transactions.
            using (var db = new FifthweekDbContext())
            {
                Trace.WriteLine("Starting with " + db.Users.Count() + " users.");
            }
        }
    }
}