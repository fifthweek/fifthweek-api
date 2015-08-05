namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveForeignKeyFromFileToUserForGarbageCollection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Files", new[] { "UserId" });
            CreateIndex("dbo.Files", "UserId");
            CreateIndex("dbo.Files", "ChannelId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Files", new[] { "ChannelId" });
            DropIndex("dbo.Files", new[] { "UserId" });
            CreateIndex("dbo.Files", "UserId");
            AddForeignKey("dbo.Files", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
