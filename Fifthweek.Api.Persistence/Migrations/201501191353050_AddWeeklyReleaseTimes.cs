namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeeklyReleaseTimes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeeklyReleaseTimes",
                c => new
                    {
                        CollectionId = c.Guid(nullable: false),
                        DayOfWeek = c.Byte(nullable: false),
                        TimeOfDay = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => new { t.CollectionId, t.DayOfWeek, t.TimeOfDay })
                .ForeignKey("dbo.Collections", t => t.CollectionId, cascadeDelete: true)
                .Index(t => t.CollectionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeeklyReleaseTimes", "CollectionId", "dbo.Collections");
            DropIndex("dbo.WeeklyReleaseTimes", new[] { "CollectionId" });
            DropTable("dbo.WeeklyReleaseTimes");
        }
    }
}
