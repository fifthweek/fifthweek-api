namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDiscoverableFieldToChannel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Channels", "IsDiscoverable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Channels", "IsDiscoverable");
        }
    }
}
