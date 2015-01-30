namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorRefreshTokens : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RefreshTokens", "IX_UsernameAndClientId");
            AddColumn("dbo.RefreshTokens", "IssuedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.RefreshTokens", "ExpiresDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RefreshTokens", "Username", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.RefreshTokens", "ClientId", c => c.String(nullable: false, maxLength: 256));
            CreateIndex("dbo.RefreshTokens", new[] { "Username", "ClientId" }, name: "IX_UsernameAndClientId");
            DropColumn("dbo.RefreshTokens", "IssuedUtc");
            DropColumn("dbo.RefreshTokens", "ExpiresUtc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefreshTokens", "ExpiresUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.RefreshTokens", "IssuedUtc", c => c.DateTime(nullable: false));
            DropIndex("dbo.RefreshTokens", "IX_UsernameAndClientId");
            AlterColumn("dbo.RefreshTokens", "ClientId", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.RefreshTokens", "Username", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.RefreshTokens", "ExpiresDate");
            DropColumn("dbo.RefreshTokens", "IssuedDate");
            CreateIndex("dbo.RefreshTokens", new[] { "Username", "ClientId" }, name: "IX_UsernameAndClientId");
        }
    }
}
