namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChannelPrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Channels");
            AddPrimaryKey("dbo.Channels", new[] { "Id", "SubscriptionId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Channels");
            AddPrimaryKey("dbo.Channels", "Id");
        }
    }
}
