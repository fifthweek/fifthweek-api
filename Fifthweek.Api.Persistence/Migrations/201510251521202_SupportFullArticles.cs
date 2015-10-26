namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SupportFullArticles : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", "IX_DTA_GetNewsfeed");
            
            DropForeignKey("dbo.Posts", "FileId", "dbo.Files");
            DropIndex("dbo.Posts", new[] { "FileId" });
            DropColumn("dbo.Posts", "FileId");
            
            RenameColumn(table: "dbo.Posts", name: "ImageId", newName: "PreviewImageId");

            RenameColumn(table: "dbo.Posts", name: "Comment", newName: "PreviewText");
            Sql("UPDATE dbo.Posts SET PreviewText = LEFT(PreviewText, 1000)");
            AlterColumn("dbo.Posts", "PreviewText", c => c.String(maxLength: 1000));

            CreateTable(
                "dbo.PostFiles",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.FileId })
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.FileId);
            
            AddColumn("dbo.Posts", "Content", c => c.String(nullable: false));
            AddColumn("dbo.Posts", "PreviewWordCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "WordCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "ImageCount", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "FileCount", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", new[] { "ChannelId", "LiveDate", "Id", "PreviewImageId", "CreationDate" }, name: "IX_DTA_GetNewsfeed");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Posts", name: "PreviewText", newName: "Comment");
            AlterColumn("dbo.Posts", "PreviewText", c => c.String(nullable: true));
            
            DropForeignKey("dbo.PostFiles", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostFiles", "FileId", "dbo.Files");
            DropIndex("dbo.PostFiles", new[] { "FileId" });
            DropIndex("dbo.PostFiles", new[] { "PostId" });
            DropIndex("dbo.Posts", "IX_DTA_GetNewsfeed");
            DropColumn("dbo.Posts", "FileCount");
            DropColumn("dbo.Posts", "ImageCount");
            DropColumn("dbo.Posts", "WordCount");
            DropColumn("dbo.Posts", "PreviewWordCount");
            DropColumn("dbo.Posts", "Content");
            DropTable("dbo.PostFiles");

            RenameColumn(table: "dbo.Posts", name: "PreviewImageId", newName: "ImageId");
            
            AddColumn("dbo.Posts", "FileId", c => c.Guid());
            CreateIndex("dbo.Posts", new[] { "FileId" }, name: "IX_FileId");
            AddForeignKey("dbo.Posts", "FileId", "dbo.Files", "Id");
            
            CreateIndex("dbo.Posts", new[] { "ChannelId", "LiveDate", "Id", "FileId", "ImageId", "CreationDate" }, name: "IX_DTA_GetNewsfeed");
        }
    }
}
