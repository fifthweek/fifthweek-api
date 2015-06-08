namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSnapshotTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreatorChannelSnapshotItems",
                c => new
                    {
                        CreatorChannelSnapshotId = c.Guid(nullable: false),
                        ChannelId = c.Guid(nullable: false),
                        PriceInUsCentsPerWeek = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CreatorChannelSnapshotId, t.ChannelId })
                .ForeignKey("dbo.CreatorChannelSnapshots", t => t.CreatorChannelSnapshotId, cascadeDelete: true)
                .Index(t => t.CreatorChannelSnapshotId);
            
            CreateTable(
                "dbo.CreatorChannelSnapshots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        CreatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CreatorGuestListSnapshotItems",
                c => new
                    {
                        CreatorGuestListSnapshotId = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => new { t.CreatorGuestListSnapshotId, t.Email })
                .ForeignKey("dbo.CreatorGuestListSnapshots", t => t.CreatorGuestListSnapshotId, cascadeDelete: true)
                .Index(t => t.CreatorGuestListSnapshotId);
            
            CreateTable(
                "dbo.CreatorGuestListSnapshots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        CreatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubscriberChannelSnapshotItems",
                c => new
                    {
                        SubscriberChannelSnapshotId = c.Guid(nullable: false),
                        ChannelId = c.Guid(nullable: false),
                        AcceptedPrice = c.Int(nullable: false),
                        SubscriptionStartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubscriberChannelSnapshotId, t.ChannelId })
                .ForeignKey("dbo.SubscriberChannelSnapshots", t => t.SubscriberChannelSnapshotId, cascadeDelete: true)
                .Index(t => t.SubscriberChannelSnapshotId);
            
            CreateTable(
                "dbo.SubscriberChannelSnapshots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        SubscriberId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubscriberSnapshots",
                c => new
                    {
                        Timestamp = c.DateTime(nullable: false),
                        SubscriberId = c.Guid(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Timestamp, t.SubscriberId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubscriberChannelSnapshotItems", "SubscriberChannelSnapshotId", "dbo.SubscriberChannelSnapshots");
            DropForeignKey("dbo.CreatorGuestListSnapshotItems", "CreatorGuestListSnapshotId", "dbo.CreatorGuestListSnapshots");
            DropForeignKey("dbo.CreatorChannelSnapshotItems", "CreatorChannelSnapshotId", "dbo.CreatorChannelSnapshots");
            DropIndex("dbo.SubscriberChannelSnapshotItems", new[] { "SubscriberChannelSnapshotId" });
            DropIndex("dbo.CreatorGuestListSnapshotItems", new[] { "CreatorGuestListSnapshotId" });
            DropIndex("dbo.CreatorChannelSnapshotItems", new[] { "CreatorChannelSnapshotId" });
            DropTable("dbo.SubscriberSnapshots");
            DropTable("dbo.SubscriberChannelSnapshots");
            DropTable("dbo.SubscriberChannelSnapshotItems");
            DropTable("dbo.CreatorGuestListSnapshots");
            DropTable("dbo.CreatorGuestListSnapshotItems");
            DropTable("dbo.CreatorChannelSnapshots");
            DropTable("dbo.CreatorChannelSnapshotItems");
        }
    }
}
