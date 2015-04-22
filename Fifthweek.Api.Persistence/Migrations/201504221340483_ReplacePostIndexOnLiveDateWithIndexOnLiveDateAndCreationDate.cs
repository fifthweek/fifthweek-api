namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplacePostIndexOnLiveDateWithIndexOnLiveDateAndCreationDate : DbMigration
    {
        public override void Up()
        {
            this.DropIndex("dbo.Posts", new[] { "LiveDate" });
            this.CreateIndex("dbo.Posts", new[] { "LiveDate", "CreationDate" }, name: "IX_LiveDateAndCreationDate");
        }
        
        public override void Down()
        {
            this.DropIndex("dbo.Posts", "IX_LiveDateAndCreationDate");
            this.CreateIndex("dbo.Posts", "LiveDate");
        }
    }
}
