namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameStripeCustomerIdColumnToPaymentOriginKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserPaymentOrigins", "PaymentOriginKey", c => c.String(maxLength: 255));
            AddColumn("dbo.UserPaymentOrigins", "PaymentOriginKeyType", c => c.Int(nullable: false));
            DropColumn("dbo.UserPaymentOrigins", "StripeCustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserPaymentOrigins", "StripeCustomerId", c => c.String(maxLength: 255));
            DropColumn("dbo.UserPaymentOrigins", "PaymentOriginKeyType");
            DropColumn("dbo.UserPaymentOrigins", "PaymentOriginKey");
        }
    }
}
