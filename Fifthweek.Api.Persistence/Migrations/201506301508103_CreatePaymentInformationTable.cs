namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePaymentInformationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserPaymentOrigins",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        StripeCustomerId = c.String(maxLength: 255),
                        BillingCountryCode = c.String(maxLength: 3),
                        CreditCardPrefix = c.String(maxLength: 6),
                        IpAddress = c.String(maxLength: 45),
                        OriginalTaxamoTransactionKey = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.AppendOnlyLedgerRecords", "StripeChargeId", c => c.String());
            AddColumn("dbo.AppendOnlyLedgerRecords", "TaxamoTransactionKey", c => c.String());
            AlterColumn("dbo.AppendOnlyLedgerRecords", "InputDataReference", c => c.Guid());
            CreateIndex("dbo.AppendOnlyLedgerRecords", "TransactionReference");
            DropColumn("dbo.AppendOnlyLedgerRecords", "StripeReference");
            DropColumn("dbo.AppendOnlyLedgerRecords", "TaxamoReference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppendOnlyLedgerRecords", "TaxamoReference", c => c.String());
            AddColumn("dbo.AppendOnlyLedgerRecords", "StripeReference", c => c.String());
            DropForeignKey("dbo.UserPaymentOrigins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserPaymentOrigins", new[] { "UserId" });
            DropIndex("dbo.AppendOnlyLedgerRecords", new[] { "TransactionReference" });
            AlterColumn("dbo.AppendOnlyLedgerRecords", "InputDataReference", c => c.Guid(nullable: false));
            DropColumn("dbo.AppendOnlyLedgerRecords", "TaxamoTransactionKey");
            DropColumn("dbo.AppendOnlyLedgerRecords", "StripeChargeId");
            DropTable("dbo.UserPaymentOrigins");
        }
    }
}
