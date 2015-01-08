namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubscriptionsAndChannels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SubscriptionId = c.Guid(nullable: false),
                        PriceInUsCentsPerWeek = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subscriptions", t => t.SubscriptionId, cascadeDelete: true)
                .Index(t => t.SubscriptionId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatorId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Tagline = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId, cascadeDelete: true)
                .Index(t => t.CreatorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Channels", "SubscriptionId", "dbo.Subscriptions");
            DropForeignKey("dbo.Subscriptions", "CreatorId", "dbo.AspNetUsers");
            DropIndex("dbo.Subscriptions", new[] { "CreatorId" });
            DropIndex("dbo.Channels", new[] { "SubscriptionId" });
            DropTable("dbo.Subscriptions");
            DropTable("dbo.Channels");
        }
    }
}
