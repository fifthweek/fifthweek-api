namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDtaRecommendedIndexForGetUserSubscriptions : DbMigration
    {
        public override void Up()
        {
            this.Sql(@"
                CREATE NONCLUSTERED INDEX [IX_DTA_GetUserSubscriptions] ON [dbo].[Collections]
                (
	                [ChannelId] ASC,
	                [Id] ASC
                )
                INCLUDE ( 	[Name]) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
            ");
        }
        
        public override void Down()
        {
            this.DropIndex("dbo.Posts", "IX_DTA_GetUserSubscriptions");
        }
    }
}
