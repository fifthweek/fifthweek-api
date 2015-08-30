namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexOnEncryptedRefreshTokenId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefreshTokens", "EncryptedId", c => c.String(maxLength: 48));
            CreateIndex("dbo.RefreshTokens", "EncryptedId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RefreshTokens", new[] { "EncryptedId" });
            AlterColumn("dbo.RefreshTokens", "EncryptedId", c => c.String(maxLength: 1024));
        }
    }
}
