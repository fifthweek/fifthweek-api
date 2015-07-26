namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveInUsCentsFromPropertyAndColumnNames : DbMigration
    {
        public override void Up()
        {
            Sql(@"EXEC sp_rename 'dbo.Channels.PriceInUsCentsPerWeek', 'Price', 'COLUMN';");
            Sql(@"EXEC sp_rename 'dbo.ChannelSubscriptions.AcceptedPriceInUsCentsPerWeek', 'AcceptedPrice', 'COLUMN';");
            Sql(@"EXEC sp_rename 'dbo.CreatorChannelsSnapshotItems.PriceInUsCentsPerWeek', 'Price', 'COLUMN';");
            Sql(@"EXEC sp_rename 'dbo.SubscriberChannelsSnapshotItems.AcceptedPriceInUsCentsPerWeek', 'AcceptedPrice', 'COLUMN';");
        }
        
        public override void Down()
        {
            Sql(@"EXEC sp_rename 'dbo.Channels.Price', 'PriceInUsCentsPerWeek', 'COLUMN';");
            Sql(@"EXEC sp_rename 'dbo.ChannelSubscriptions.AcceptedPrice', 'AcceptedPriceInUsCentsPerWeek', 'COLUMN';");
            Sql(@"EXEC sp_rename 'dbo.CreatorChannelsSnapshotItems.Price', 'PriceInUsCentsPerWeek', 'COLUMN';");
            Sql(@"EXEC sp_rename 'dbo.SubscriberChannelsSnapshotItems.AcceptedPrice', 'AcceptedPriceInUsCentsPerWeek', 'COLUMN';");
        }
    }
}
