namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSnapshotTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CreatorChannelsSnapshotItems",
                c => new
                    {
                        CreatorChannelsSnapshotId = c.Guid(nullable: false),
                        ChannelId = c.Guid(nullable: false),
                        PriceInUsCentsPerWeek = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CreatorChannelsSnapshotId, t.ChannelId })
                .ForeignKey("dbo.CreatorChannelsSnapshots", t => t.CreatorChannelsSnapshotId, cascadeDelete: true)
                .Index(t => t.CreatorChannelsSnapshotId);
            
            CreateTable(
                "dbo.CreatorChannelsSnapshots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        CreatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.CreatorId, t.Timestamp }, name: "CreatorIdAndTimestamp");
            
            CreateTable(
                "dbo.CreatorFreeAccessUsersSnapshotItems",
                c => new
                    {
                        CreatorFreeAccessUsersSnapshotId = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => new { t.CreatorFreeAccessUsersSnapshotId, t.Email })
                .ForeignKey("dbo.CreatorFreeAccessUsersSnapshots", t => t.CreatorFreeAccessUsersSnapshotId, cascadeDelete: true)
                .Index(t => t.CreatorFreeAccessUsersSnapshotId);
            
            CreateTable(
                "dbo.CreatorFreeAccessUsersSnapshots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        CreatorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.CreatorId, t.Timestamp }, name: "CreatorIdAndTimestamp");
            
            CreateTable(
                "dbo.SubscriberChannelsSnapshotItems",
                c => new
                    {
                        SubscriberChannelsSnapshotId = c.Guid(nullable: false),
                        ChannelId = c.Guid(nullable: false),
                        AcceptedPriceInUsCentsPerWeek = c.Int(nullable: false),
                        SubscriptionStartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubscriberChannelsSnapshotId, t.ChannelId })
                .ForeignKey("dbo.SubscriberChannelsSnapshots", t => t.SubscriberChannelsSnapshotId, cascadeDelete: true)
                .Index(t => t.SubscriberChannelsSnapshotId);
            
            CreateTable(
                "dbo.SubscriberChannelsSnapshots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        SubscriberId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.SubscriberId, t.Timestamp }, name: "SubscriberIdAndTimestamp");
            
            CreateTable(
                "dbo.SubscriberSnapshots",
                c => new
                    {
                        SubscriberId = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => new { t.SubscriberId, t.Timestamp });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubscriberChannelsSnapshotItems", "SubscriberChannelsSnapshotId", "dbo.SubscriberChannelsSnapshots");
            DropForeignKey("dbo.CreatorFreeAccessUsersSnapshotItems", "CreatorFreeAccessUsersSnapshotId", "dbo.CreatorFreeAccessUsersSnapshots");
            DropForeignKey("dbo.CreatorChannelsSnapshotItems", "CreatorChannelsSnapshotId", "dbo.CreatorChannelsSnapshots");
            DropIndex("dbo.SubscriberChannelsSnapshots", "SubscriberIdAndTimestamp");
            DropIndex("dbo.SubscriberChannelsSnapshotItems", new[] { "SubscriberChannelsSnapshotId" });
            DropIndex("dbo.CreatorFreeAccessUsersSnapshots", "CreatorIdAndTimestamp");
            DropIndex("dbo.CreatorFreeAccessUsersSnapshotItems", new[] { "CreatorFreeAccessUsersSnapshotId" });
            DropIndex("dbo.CreatorChannelsSnapshots", "CreatorIdAndTimestamp");
            DropIndex("dbo.CreatorChannelsSnapshotItems", new[] { "CreatorChannelsSnapshotId" });
            DropTable("dbo.SubscriberSnapshots");
            DropTable("dbo.SubscriberChannelsSnapshots");
            DropTable("dbo.SubscriberChannelsSnapshotItems");
            DropTable("dbo.CreatorFreeAccessUsersSnapshots");
            DropTable("dbo.CreatorFreeAccessUsersSnapshotItems");
            DropTable("dbo.CreatorChannelsSnapshots");
            DropTable("dbo.CreatorChannelsSnapshotItems");
        }
    }
}
