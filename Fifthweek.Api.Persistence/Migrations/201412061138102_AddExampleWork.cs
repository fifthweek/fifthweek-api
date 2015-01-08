using System.Data.Entity.Migrations;

namespace Fifthweek.Api.Persistence.Migrations
{
    public partial class AddExampleWork : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.AspNetUsers", "ExampleWork", c => c.String());
        }
        
        public override void Down()
        {
            this.DropColumn("dbo.AspNetUsers", "ExampleWork");
        }
    }
}
