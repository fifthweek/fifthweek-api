namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTransactionTypeColumnAndSplitCreditAndRevenue : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AppendOnlyLedgerRecords", "UniqueKey");
            AddColumn("dbo.AppendOnlyLedgerRecords", "TransactionType", c => c.Int(nullable: false));
            CreateIndex("dbo.AppendOnlyLedgerRecords", new[] { "AccountOwnerId", "CounterpartyId", "TransactionType", "Timestamp", "Amount", "AccountType" }, unique: true, name: "UniqueKey");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AppendOnlyLedgerRecords", "UniqueKey");
            DropColumn("dbo.AppendOnlyLedgerRecords", "TransactionType");
            CreateIndex("dbo.AppendOnlyLedgerRecords", new[] { "AccountOwnerId", "CounterpartyId", "Timestamp", "Amount", "AccountType" }, unique: true, name: "UniqueKey");
        }
    }
}
