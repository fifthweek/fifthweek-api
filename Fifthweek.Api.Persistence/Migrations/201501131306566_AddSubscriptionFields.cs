namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubscriptionFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "Introduction", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.Subscriptions", "Description", c => c.String(maxLength: 2000));
            AddColumn("dbo.Subscriptions", "ExternalVideoUrl", c => c.String(maxLength: 100));
            AddColumn("dbo.Subscriptions", "HeaderImageFileId", c => c.Guid());
            AlterColumn("dbo.Subscriptions", "Name", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Subscriptions", "Tagline", c => c.String(nullable: false, maxLength: 55));
            CreateIndex("dbo.Subscriptions", "HeaderImageFileId");
            AddForeignKey("dbo.Subscriptions", "HeaderImageFileId", "dbo.Files", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriptions", "HeaderImageFileId", "dbo.Files");
            DropIndex("dbo.Subscriptions", new[] { "HeaderImageFileId" });
            AlterColumn("dbo.Subscriptions", "Tagline", c => c.String(nullable: false));
            AlterColumn("dbo.Subscriptions", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Subscriptions", "HeaderImageFileId");
            DropColumn("dbo.Subscriptions", "ExternalVideoUrl");
            DropColumn("dbo.Subscriptions", "Description");
            DropColumn("dbo.Subscriptions", "Introduction");
        }
    }
}
