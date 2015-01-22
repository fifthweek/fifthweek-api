namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameFieldToChannels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Channels", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Channels", "Name");
        }
    }
}
