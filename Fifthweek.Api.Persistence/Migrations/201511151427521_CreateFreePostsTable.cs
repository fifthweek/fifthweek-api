namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateFreePostsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FreePosts",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        PostId = c.Guid(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.PostId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => new { t.UserId, t.Timestamp }, name: "IX_DTA_CountFreePostsOnDate")
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FreePosts", "PostId", "dbo.Posts");
            DropIndex("dbo.FreePosts", new[] { "PostId" });
            DropIndex("dbo.FreePosts", "IX_DTA_CountFreePostsOnDate");
            DropTable("dbo.FreePosts");
        }
    }
}
