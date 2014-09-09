namespace Dexter.Api
{
    using System;
    using System.Collections.Generic;

    using Dexter.Api.Entities;
    using Dexter.Api.Models;

    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DexterDbContext>
    {
        protected override void Seed(DexterDbContext context)
        {
            var clients = new List<Client> 
            {
                new Client
                {
                    Id = "dexter.web.1",
                    Secret = Helper.GetHash(Guid.NewGuid().ToString()),
                    Name = "Dexter Website",
                    ApplicationType = ApplicationType.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = (int)TimeSpan.FromDays(365).TotalMinutes,
                    AllowedOrigin = Constants.DexterWebsiteOrigin,
                }
            };

            clients.ForEach(v => context.Clients.Add(v));
            context.SaveChanges();
        }
    }
}