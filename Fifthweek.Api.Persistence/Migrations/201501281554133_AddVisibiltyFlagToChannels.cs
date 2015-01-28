namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVisibiltyFlagToChannels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Channels", "IsVisibleToNonSubscribers", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Channels", "IsVisibleToNonSubscribers");
        }
    }
}
