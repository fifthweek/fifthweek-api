namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeIntroductionOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "Introduction", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Blogs", "Introduction", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
