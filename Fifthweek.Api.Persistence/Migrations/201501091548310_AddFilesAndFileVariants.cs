namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFilesAndFileVariants : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        State = c.Int(nullable: false),
                        UploadStartedDate = c.DateTime(nullable: false),
                        UploadCompletedDate = c.DateTime(),
                        ProcessingStartedDate = c.DateTime(),
                        ProcessingCompletedDate = c.DateTime(),
                        FileNameWithoutExtension = c.String(nullable: false),
                        FileExtension = c.String(nullable: false),
                        BlobSizeBytes = c.Long(nullable: false),
                        Purpose = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FileVariants",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                        BlobSizeBytes = c.Long(nullable: false),
                        MimeType = c.String(nullable: false),
                        VariantType = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Files", t => t.FileId, cascadeDelete: true)
                .Index(t => t.FileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileVariants", "FileId", "dbo.Files");
            DropForeignKey("dbo.Files", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.FileVariants", new[] { "FileId" });
            DropIndex("dbo.Files", new[] { "UserId" });
            DropTable("dbo.FileVariants");
            DropTable("dbo.Files");
        }
    }
}
