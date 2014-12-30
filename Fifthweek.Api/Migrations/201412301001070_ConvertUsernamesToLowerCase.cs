namespace Fifthweek.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConvertUsernamesToLowerCase : DbMigration
    {
        public override void Up()
        {
            this.Sql("UPDATE AspNetUsers SET UserName = LOWER(UserName)");
            this.Sql("UPDATE RefreshTokens SET Username = LOWER(Username)");
        }
        
        public override void Down()
        {
            // There is no way to undo this migration.
        }
    }
}
