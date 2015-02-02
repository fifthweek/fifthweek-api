namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChannelDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Channels", "Description", c => c.String(maxLength: 250));
            DropColumn("dbo.Channels", "Name");
            AddColumn("dbo.Channels", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Channels", "Name");
            AddColumn("dbo.Channels", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Channels", "Description");
        }
    }
}
