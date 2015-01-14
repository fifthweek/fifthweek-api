namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostsAndCollections : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Channels", new[] { "SubscriptionId" });
            DropIndex("dbo.Subscriptions", new[] { "CreatorId" });
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChannelId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Channels", t => t.ChannelId, cascadeDelete: true)
                .Index(t => t.ChannelId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChannelId = c.Guid(nullable: false),
                        CollectionId = c.Guid(),
                        FileId = c.Guid(),
                        ImageId = c.Guid(),
                        Comment = c.String(maxLength: 280),
                        QueuePosition = c.Int(),
                        LiveDate = c.DateTime(),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Channels", t => t.ChannelId, cascadeDelete: true)
                .ForeignKey("dbo.Collections", t => t.CollectionId)
                .ForeignKey("dbo.Files", t => t.FileId)
                .ForeignKey("dbo.Files", t => t.ImageId)
                .Index(t => t.ChannelId)
                .Index(t => t.CollectionId)
                .Index(t => t.LiveDate);
            
            CreateIndex("dbo.Channels", "SubscriptionId");
            CreateIndex("dbo.Subscriptions", "CreatorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "ImageId", "dbo.Files");
            DropForeignKey("dbo.Posts", "FileId", "dbo.Files");
            DropForeignKey("dbo.Posts", "CollectionId", "dbo.Collections");
            DropForeignKey("dbo.Posts", "ChannelId", "dbo.Channels");
            DropForeignKey("dbo.Collections", "ChannelId", "dbo.Channels");
            DropIndex("dbo.Posts", new[] { "LiveDate" });
            DropIndex("dbo.Posts", new[] { "CollectionId" });
            DropIndex("dbo.Posts", new[] { "ChannelId" });
            DropIndex("dbo.Collections", new[] { "ChannelId" });
            DropIndex("dbo.Subscriptions", new[] { "CreatorId" });
            DropIndex("dbo.Channels", new[] { "SubscriptionId" });
            DropTable("dbo.Posts");
            DropTable("dbo.Collections");
            CreateIndex("dbo.Subscriptions", "CreatorId");
            CreateIndex("dbo.Channels", "SubscriptionId");
        }
    }
}
