namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCollectionsToQueuesAndSimplifyBlogFieldsPart2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeeklyReleaseTimes",
                c => new
                    {
                        QueueId = c.Guid(nullable: false),
                        HourOfWeek = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.QueueId, t.HourOfWeek })
                .ForeignKey("dbo.Queues", t => t.QueueId, cascadeDelete: true)
                .Index(t => t.QueueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeeklyReleaseTimes", "QueueId", "dbo.Queues");
            DropIndex("dbo.WeeklyReleaseTimes", new[] { "QueueId" });
            DropTable("dbo.WeeklyReleaseTimes");
        }
    }
}
