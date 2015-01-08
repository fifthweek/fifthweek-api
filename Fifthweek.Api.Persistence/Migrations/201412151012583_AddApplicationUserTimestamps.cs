using System;
using System.Data.Entity.Migrations;

namespace Fifthweek.Api.Persistence.Migrations
{
    public partial class AddApplicationUserTimestamps : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.AspNetUsers", "RegistrationDate", c => c.DateTime(nullable: false));
            this.AddColumn("dbo.AspNetUsers", "LastSignInDate", c => c.DateTime(nullable: false));
            this.AddColumn("dbo.AspNetUsers", "LastAccessTokenDate", c => c.DateTime(nullable: false));

            // Configure existing users to have a reasonable initial set of dates.
            var initialDate = new DateTime(2014, 12, 1, 12, 0, 0, DateTimeKind.Utc).ToString("yyyy-MM-dd HH:mm:ss");
            
            this.Sql("UPDATE [dbo].[AspNetUsers] SET RegistrationDate = '" + initialDate + "'");
            this.Sql("UPDATE [dbo].[AspNetUsers] SET LastSignInDate = '" + initialDate + "'");
            this.Sql("UPDATE [dbo].[AspNetUsers] SET LastAccessTokenDate = '" + initialDate + "'");
        }
        
        public override void Down()
        {
            this.DropColumn("dbo.AspNetUsers", "LastAccessTokenDate");
            this.DropColumn("dbo.AspNetUsers", "LastSignInDate");
            this.DropColumn("dbo.AspNetUsers", "RegistrationDate");
        }
    }
}
