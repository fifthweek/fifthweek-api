namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReduceLengthOfRefreshTokenClientIdToFixIndexSize : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RefreshTokens", "IX_UsernameAndClientId");
            AlterColumn("dbo.RefreshTokens", "ClientId", c => c.String(nullable: false, maxLength: 64));
            CreateIndex("dbo.RefreshTokens", new[] { "Username", "ClientId" }, name: "IX_UsernameAndClientId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RefreshTokens", "IX_UsernameAndClientId");
            AlterColumn("dbo.RefreshTokens", "ClientId", c => c.String(nullable: false, maxLength: 256));
            CreateIndex("dbo.RefreshTokens", new[] { "Username", "ClientId" }, name: "IX_UsernameAndClientId");
        }
    }
}
