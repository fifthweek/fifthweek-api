namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePaymentTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppendOnlyLedgerRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccountOwnerId = c.Guid(nullable: false),
                        CounterpartyId = c.Guid(),
                        Timestamp = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccountType = c.Int(nullable: false),
                        TransactionReference = c.Guid(nullable: false),
                        InputDataReference = c.Guid(nullable: false),
                        Comment = c.String(),
                        StripeReference = c.String(),
                        TaxamoReference = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Timestamp, t.AccountOwnerId }, name: "TimestampAndAccountOwner")
                .Index(t => new { t.AccountOwnerId, t.CounterpartyId, t.Timestamp, t.Amount, t.AccountType }, unique: true, name: "UniqueKey");
            
            CreateTable(
                "dbo.CalculatedAccountBalances",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        AccountType = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.UserId, t.AccountType, t.Timestamp });
            
            CreateTable(
                "dbo.CreatorPercentageOverrides",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        Percentage = c.Decimal(nullable: false, precision: 9, scale: 9),
                        ExpiryDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UncommittedSubscriptionPayments",
                c => new
                    {
                        SubscriberId = c.Guid(nullable: false),
                        CreatorId = c.Guid(nullable: false),
                        StartTimestampInclusive = c.DateTime(nullable: false),
                        EndTimestampExclusive = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InputDataReference = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubscriberId, t.CreatorId });
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AppendOnlyLedgerRecords", "UniqueKey");
            DropIndex("dbo.AppendOnlyLedgerRecords", "TimestampAndAccountOwner");
            DropTable("dbo.UncommittedSubscriptionPayments");
            DropTable("dbo.CreatorPercentageOverrides");
            DropTable("dbo.CalculatedAccountBalances");
            DropTable("dbo.AppendOnlyLedgerRecords");
        }
    }
}
