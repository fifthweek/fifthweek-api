namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSubscriptionRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Subscriptions", "Name", c => c.String());
            AlterColumn("dbo.Subscriptions", "Tagline", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Subscriptions", "Tagline", c => c.String(nullable: false));
            AlterColumn("dbo.Subscriptions", "Name", c => c.String(nullable: false));
        }
    }
}
