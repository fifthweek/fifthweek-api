namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQueueExclusiveLowerBoundToCollections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Collections", "QueueExclusiveLowerBound", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Collections", "QueueExclusiveLowerBound");
        }
    }
}
