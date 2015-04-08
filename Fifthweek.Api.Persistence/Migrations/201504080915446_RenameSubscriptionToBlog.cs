namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSubscriptionToBlog : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Subscriptions", newName: "Blogs");
            RenameColumn(table: "dbo.Channels", name: "SubscriptionId", newName: "BlogId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Channels", name: "BlogId", newName: "SubscriptionId");
            RenameTable(name: "dbo.Blogs", newName: "Subscriptions");
        }
    }
}
