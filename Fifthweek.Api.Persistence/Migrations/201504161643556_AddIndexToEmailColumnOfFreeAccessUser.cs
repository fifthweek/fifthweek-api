namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToEmailColumnOfFreeAccessUser : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.FreeAccessUsers", "Email");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FreeAccessUsers", new[] { "Email" });
        }
    }
}
