namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEndToEndTestEmails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EndToEndTestEmails",
                c => new
                    {
                        Mailbox = c.String(nullable: false, maxLength: 16),
                        Subject = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Mailbox);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EndToEndTestEmails");
        }
    }
}
