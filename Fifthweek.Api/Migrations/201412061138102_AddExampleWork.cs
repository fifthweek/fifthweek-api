namespace Fifthweek.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExampleWork : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ExampleWork", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ExampleWork");
        }
    }
}
