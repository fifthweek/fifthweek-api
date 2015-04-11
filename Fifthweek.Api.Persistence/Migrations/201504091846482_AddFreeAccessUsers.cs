namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFreeAccessUsers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FreeAccessUsers",
                c => new
                    {
                        BlogId = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => new { t.BlogId, t.Email })
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FreeAccessUsers", "BlogId", "dbo.Blogs");
            DropIndex("dbo.FreeAccessUsers", new[] { "BlogId" });
            DropTable("dbo.FreeAccessUsers");
        }
    }
}
