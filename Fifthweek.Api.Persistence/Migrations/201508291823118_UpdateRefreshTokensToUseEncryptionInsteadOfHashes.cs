namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRefreshTokensToUseEncryptionInsteadOfHashes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RefreshTokens", "IX_UsernameAndClientId");
            DropTable("dbo.RefreshTokens");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        HashedId = c.String(nullable: false, maxLength: 128),
                        Username = c.String(nullable: false, maxLength: 256),
                        ClientId = c.String(nullable: false, maxLength: 64),
                        IssuedDate = c.DateTime(nullable: false),
                        ExpiresDate = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HashedId);
            
            CreateIndex("dbo.RefreshTokens", new[] { "Username", "ClientId" }, name: "IX_UsernameAndClientId");
        }
    }
}
