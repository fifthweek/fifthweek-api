namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChannelIdToFilesToEnableLocatingOrphanedFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "ChannelId", c => c.Guid());
            Sql("UPDATE f SET f.ChannelId=p.ChannelId FROM Files f LEFT JOIN Posts p ON p.ImageId=f.Id OR p.FileId=f.Id");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "ChannelId");
        }
    }
}
