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
            var clients = new List<Client> 
            {
                new Client
                {
                    Id = "fifthweek.web.1",
                    Secret = Helper.GetHash(Guid.NewGuid().ToString()),
                    Name = "Fifthweek Website",
                    ApplicationType = ApplicationType.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = (int)TimeSpan.FromDays(365).TotalMinutes,
                    AllowedOrigin = Constants.FifthweekWebsiteOrigin,
                }
            };

            clients.ForEach(v => context.Clients.Add(v));
            context.SaveChanges();
        }
    }
}