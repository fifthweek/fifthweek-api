namespace Fifthweek.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplaceRefreshTokensManualIndexWithCodeFirstIndex : DbMigration
    {
        public override void Up()
        {
            this.DropIndex("dbo.RefreshTokens", "UsernameClientIdIndex");
            this.CreateIndex("dbo.RefreshTokens", new[] { "Username", "ClientId" }, name: "IX_UsernameAndClientId");
        }
        
        public override void Down()
        {
            this.DropIndex("dbo.RefreshTokens", "IX_UsernameAndClientId");
            this.CreateIndex("dbo.RefreshTokens", new[] { "Username", "ClientId" }, name: "UsernameClientIdIndex");
        }
    }
}
