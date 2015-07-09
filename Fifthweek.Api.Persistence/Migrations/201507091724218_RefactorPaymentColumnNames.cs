namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorPaymentColumnNames : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserPaymentOrigins", new[] { "BillingStatus" });
            AddColumn("dbo.UserPaymentOrigins", "CountryCode", c => c.String(maxLength: 3));
            AddColumn("dbo.UserPaymentOrigins", "PaymentStatus", c => c.Int(nullable: false));
            CreateIndex("dbo.UserPaymentOrigins", "PaymentStatus");
            DropColumn("dbo.UserPaymentOrigins", "BillingCountryCode");
            DropColumn("dbo.UserPaymentOrigins", "BillingStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPaymentOrigins", "BillingStatus", c => c.Int(nullable: false));
            AddColumn("dbo.UserPaymentOrigins", "BillingCountryCode", c => c.String(maxLength: 3));
            DropIndex("dbo.UserPaymentOrigins", new[] { "PaymentStatus" });
            DropColumn("dbo.UserPaymentOrigins", "PaymentStatus");
            DropColumn("dbo.UserPaymentOrigins", "CountryCode");
            CreateIndex("dbo.UserPaymentOrigins", "BillingStatus");
        }
    }
}
