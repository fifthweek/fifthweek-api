namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChannelPrimaryKey2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Channels");
            AddPrimaryKey("dbo.Channels", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Channels");
            AddPrimaryKey("dbo.Channels", new[] { "Id", "SubscriptionId" });
        }
    }
}
