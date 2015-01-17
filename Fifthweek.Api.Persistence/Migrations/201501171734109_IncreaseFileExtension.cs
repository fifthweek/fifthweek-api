namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseFileExtension : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Files", "FileExtension", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "FileExtension", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
