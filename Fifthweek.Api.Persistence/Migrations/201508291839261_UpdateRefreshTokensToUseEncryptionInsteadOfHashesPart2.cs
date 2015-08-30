namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRefreshTokensToUseEncryptionInsteadOfHashesPart2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 256),
                        ClientId = c.String(nullable: false, maxLength: 64),
                        EncryptedId = c.String(maxLength: 1024),
                        IssuedDate = c.DateTime(nullable: false),
                        ExpiresDate = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Username, t.ClientId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RefreshTokens");
        }
    }
}
