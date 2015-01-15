namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFileVariants : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileVariants", "FileId", "dbo.Files");
            DropIndex("dbo.FileVariants", new[] { "FileId" });
            DropTable("dbo.FileVariants");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.FileVariants", "FileId");
            AddForeignKey("dbo.FileVariants", "FileId", "dbo.Files", "Id", cascadeDelete: true);
        }
    }
}
