namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCascadeDeleteToUserPaymentOrigin : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserPaymentOrigins", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.UserPaymentOrigins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserPaymentOrigins", "UserId", "dbo.AspNetUsers");
            AddForeignKey("dbo.UserPaymentOrigins", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
