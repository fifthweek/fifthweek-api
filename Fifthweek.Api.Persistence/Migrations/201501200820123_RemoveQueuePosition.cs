namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveQueuePosition : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "LiveDate" });
            AddColumn("dbo.Posts", "ScheduledByQueue", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Posts", "LiveDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Posts", "LiveDate");
            DropColumn("dbo.Posts", "QueuePosition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "QueuePosition", c => c.Int());
            DropIndex("dbo.Posts", new[] { "LiveDate" });
            AlterColumn("dbo.Posts", "LiveDate", c => c.DateTime());
            DropColumn("dbo.Posts", "ScheduledByQueue");
            CreateIndex("dbo.Posts", "LiveDate");
        }
    }
}
