namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreasePreviewTextTo1500Characters : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "PreviewText", c => c.String(maxLength: 1500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "PreviewText", c => c.String(maxLength: 1000));
        }
    }
}
