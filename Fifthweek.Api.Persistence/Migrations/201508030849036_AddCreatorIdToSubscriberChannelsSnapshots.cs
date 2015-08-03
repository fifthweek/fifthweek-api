namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatorIdToSubscriberChannelsSnapshots : DbMigration
    {
        public override void Up()
        {
            // At this stage there are no useful snapshots, so just remove what we have.
            Sql("DELETE FROM dbo.SubscriberChannelsSnapshotItems");
            AddColumn("dbo.SubscriberChannelsSnapshotItems", "CreatorId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubscriberChannelsSnapshotItems", "CreatorId");
        }
    }
}
