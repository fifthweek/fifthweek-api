namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideoCountToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "VideoCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "VideoCount");
        }
    }
}
