namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBillingStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPaymentOrigins", "BillingStatus", c => c.Int(nullable: false));
            CreateIndex("dbo.UserPaymentOrigins", "BillingStatus");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserPaymentOrigins", new[] { "BillingStatus" });
            DropColumn("dbo.UserPaymentOrigins", "BillingStatus");
        }
    }
}
