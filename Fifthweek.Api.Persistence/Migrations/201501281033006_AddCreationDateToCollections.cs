namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreationDateToCollections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Collections", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Collections", "CreationDate");
        }
    }
}
