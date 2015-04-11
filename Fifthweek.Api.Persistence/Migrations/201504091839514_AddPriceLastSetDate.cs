namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceLastSetDate : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.Channels", "PriceLastSetDate", c => c.DateTime(nullable: false));
            this.Sql("UPDATE dbo.Channels SET PriceLastSetDate = CreationDate");
        }
        
        public override void Down()
        {
            this.DropColumn("dbo.Channels", "PriceLastSetDate");
        }
    }
}
