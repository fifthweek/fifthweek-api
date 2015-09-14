namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCollectionsToQueuesAndSimplifyBlogFields : DbMigration
    {
        public override void Up()
        {
            this.DropIndex("dbo.Posts", "IX_DTA_GetNewsfeed");

            DropForeignKey("dbo.Collections", "ChannelId", "dbo.Channels");
            DropForeignKey("dbo.Posts", "CollectionId", "dbo.Collections");
            DropForeignKey("dbo.WeeklyReleaseTimes", "CollectionId", "dbo.Collections");
            DropIndex("dbo.Collections", new[] { "ChannelId" });
            DropIndex("dbo.Posts", new[] { "CollectionId" });
            DropIndex("dbo.WeeklyReleaseTimes", new[] { "CollectionId" });
            CreateTable(
                "dbo.Queues",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BlogId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            AddColumn("dbo.Posts", "QueueId", c => c.Guid());
            AlterColumn("dbo.Blogs", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Posts", "QueueId");
            AddForeignKey("dbo.Posts", "QueueId", "dbo.Queues", "Id");
            DropColumn("dbo.Blogs", "Tagline");
            DropColumn("dbo.Channels", "Description");
            DropColumn("dbo.Posts", "CollectionId");
            DropColumn("dbo.Posts", "ScheduledByQueue");
            DropTable("dbo.Collections");
            DropTable("dbo.WeeklyReleaseTimes");

            this.Sql(@"
                CREATE NONCLUSTERED INDEX [IX_DTA_GetNewsfeed] ON [dbo].[Posts]
                (
	                [ChannelId] ASC,
	                [LiveDate] ASC,
	                [Id] ASC,
	                [FileId] ASC,
	                [ImageId] ASC,
	                [CreationDate] ASC
                )
                INCLUDE ( 	[QueueId],
	                [Comment]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
            ");
        }
        
        public override void Down()
        {
            this.DropIndex("dbo.Posts", "IX_DTA_GetNewsfeed");

            CreateTable(
                "dbo.WeeklyReleaseTimes",
                c => new
                    {
                        CollectionId = c.Guid(nullable: false),
                        HourOfWeek = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.CollectionId, t.HourOfWeek });
            
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ChannelId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        QueueExclusiveLowerBound = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Posts", "ScheduledByQueue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Posts", "CollectionId", c => c.Guid());
            AddColumn("dbo.Channels", "Description", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Blogs", "Tagline", c => c.String(nullable: false, maxLength: 55));
            DropForeignKey("dbo.Posts", "QueueId", "dbo.Queues");
            DropForeignKey("dbo.Queues", "BlogId", "dbo.Blogs");
            DropIndex("dbo.Queues", new[] { "BlogId" });
            DropIndex("dbo.Posts", new[] { "QueueId" });
            AlterColumn("dbo.Blogs", "Name", c => c.String(nullable: false, maxLength: 25));
            DropColumn("dbo.Posts", "QueueId");
            DropTable("dbo.Queues");
            CreateIndex("dbo.WeeklyReleaseTimes", "CollectionId");
            CreateIndex("dbo.Posts", "CollectionId");
            CreateIndex("dbo.Collections", "ChannelId");
            AddForeignKey("dbo.WeeklyReleaseTimes", "CollectionId", "dbo.Collections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posts", "CollectionId", "dbo.Collections", "Id");
            AddForeignKey("dbo.Collections", "ChannelId", "dbo.Channels", "Id", cascadeDelete: true);

            this.Sql(@"
                CREATE NONCLUSTERED INDEX [IX_DTA_GetNewsfeed] ON [dbo].[Posts]
                (
	                [ChannelId] ASC,
	                [LiveDate] ASC,
	                [Id] ASC,
	                [FileId] ASC,
	                [ImageId] ASC,
	                [CreationDate] ASC
                )
                INCLUDE ( 	[CollectionId],
	                [Comment]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
            ");
        }
    }
}
