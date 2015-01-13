namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfileImageFileIdToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ProfileImageFileId", c => c.Guid());
            AddForeignKey("dbo.AspNetUsers", "ProfileImageFileId", "dbo.Files", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "ProfileImageFileId", "dbo.Files");
            DropColumn("dbo.AspNetUsers", "ProfileImageFileId");
        }
    }
}
