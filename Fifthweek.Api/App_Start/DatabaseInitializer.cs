namespace Fifthweek.Api
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Repositories;

    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FifthweekDbContext>
    {
        protected override void Seed(FifthweekDbContext context)
        {
        }
    }
}