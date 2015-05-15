namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnusedIndexOnPostsAndReplaceWithDtaRecommendedIndex : DbMigration
    {
        public override void Up()
        {
            this.DropIndex("dbo.Posts", "IX_LiveDateAndCreationDate");
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
        
        public override void Down()
        {
            this.CreateIndex("dbo.Posts", new[] { "LiveDate", "CreationDate" }, name: "IX_LiveDateAndCreationDate");
            this.DropIndex("dbo.Posts", "IX_DTA_GetNewsfeed");
        }
    }
}
