namespace Fifthweek.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRefreshTokensIndexForUsernameClientId : DbMigration
    {
        public override void Up()
        {
            this.CreateIndex("RefreshTokens", new string[] { "Username", "ClientId" }, name: "UsernameClientIdIndex");
        }
        
        public override void Down()
        {
            this.DropIndex("RefreshTokens", "UsernameClientIdIndex");
        }
    }
}
