namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseLongFormTextFieldSizes : DbMigration
    {
        public override void Up()
        {
            this.DropIndex("dbo.Posts", "IX_DTA_GetNewsfeed");
            DropIndex("dbo.Posts", new[] { "FileId" });
            DropIndex("dbo.Posts", new[] { "ImageId" });
            AlterColumn("dbo.Blogs", "Description", c => c.String());
            AlterColumn("dbo.Comments", "Content", c => c.String());
            AlterColumn("dbo.Posts", "Comment", c => c.String());
            CreateIndex("dbo.Posts", new[] { "ChannelId", "LiveDate", "Id", "FileId", "ImageId", "CreationDate" }, name: "IX_DTA_GetNewsfeed");
            CreateIndex("dbo.Posts", "FileId");
            CreateIndex("dbo.Posts", "ImageId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "ImageId" });
            DropIndex("dbo.Posts", new[] { "FileId" });
            DropIndex("dbo.Posts", "IX_DTA_GetNewsfeed");
            AlterColumn("dbo.Posts", "Comment", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Comments", "Content", c => c.String(maxLength: 2000));
            AlterColumn("dbo.Blogs", "Description", c => c.String(maxLength: 2000));
            CreateIndex("dbo.Posts", "ImageId");
            CreateIndex("dbo.Posts", "FileId");
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
    }
}
