namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeChannelNameAndDescriptionRequired : DbMigration
    {
        public override void Up()
        {
            Sql(string.Format("UPDATE [dbo].[Channels] SET Name='{0}', Description='{1}'", "Basic Subscription", "Exclusive News Feed" + Environment.NewLine + "Early Updates on New Releases"));
            AlterColumn("dbo.Channels", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Channels", "Description", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Channels", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Channels", "Name", c => c.String(maxLength: 50));
        }
    }
}
