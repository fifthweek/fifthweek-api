namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChannelSubscriptionsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChannelSubscriptions",
                c => new
                    {
                        ChannelId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        AcceptedPriceInUsCentsPerWeek = c.Int(nullable: false),
                        PriceLastAcceptedDate = c.DateTime(nullable: false),
                        SubscriptionStartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ChannelId, t.UserId })
                .ForeignKey("dbo.Channels", t => t.ChannelId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ChannelId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChannelSubscriptions", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChannelSubscriptions", "ChannelId", "dbo.Channels");
            DropIndex("dbo.ChannelSubscriptions", new[] { "UserId" });
            DropIndex("dbo.ChannelSubscriptions", new[] { "ChannelId" });
            DropTable("dbo.ChannelSubscriptions");
        }
    }
}
