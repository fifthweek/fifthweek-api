namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileProcessingInformationToFilesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "ProcessingAttempts", c => c.Int());
            AddColumn("dbo.Files", "RenderWidth", c => c.Int());
            AddColumn("dbo.Files", "RenderHeight", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "RenderHeight");
            DropColumn("dbo.Files", "RenderWidth");
            DropColumn("dbo.Files", "ProcessingAttempts");
        }
    }
}
