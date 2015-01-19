namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetWeeklyReleaseTimesToUseHourOfWeek : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.WeeklyReleaseTimes");
            AddColumn("dbo.WeeklyReleaseTimes", "HourOfWeek", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.WeeklyReleaseTimes", new[] { "CollectionId", "HourOfWeek" });
            DropColumn("dbo.WeeklyReleaseTimes", "DayOfWeek");
            DropColumn("dbo.WeeklyReleaseTimes", "TimeOfDay");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WeeklyReleaseTimes", "TimeOfDay", c => c.Time(nullable: false, precision: 7));
            AddColumn("dbo.WeeklyReleaseTimes", "DayOfWeek", c => c.Byte(nullable: false));
            DropPrimaryKey("dbo.WeeklyReleaseTimes");
            DropColumn("dbo.WeeklyReleaseTimes", "HourOfWeek");
            AddPrimaryKey("dbo.WeeklyReleaseTimes", new[] { "CollectionId", "DayOfWeek", "TimeOfDay" });
        }
    }
}
