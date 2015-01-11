namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSubscriptionRequiredFields2 : DbMigration
    {
        public override void Up()
        {
            this.Sql("DELETE FROM [dbo].[Subscriptions] WHERE [Name] IS NULL");

            AlterColumn("dbo.Subscriptions", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Subscriptions", "Tagline", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "Tagline", c => c.String());
            AlterColumn("dbo.Subscriptions", "Name", c => c.String());
        }
    }
}
