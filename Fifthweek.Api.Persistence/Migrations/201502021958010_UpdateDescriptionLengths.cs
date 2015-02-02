namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDescriptionLengths : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Channels", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Collections", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Collections", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Channels", "Name", c => c.String(nullable: false, maxLength: 25));
        }
    }
}
