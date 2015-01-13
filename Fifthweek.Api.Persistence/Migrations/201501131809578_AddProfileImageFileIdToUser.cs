namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfileImageFileIdToUser : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.AspNetUsers", "ProfileImageFileId", c => c.Guid());
            this.AddForeignKey("dbo.AspNetUsers", "ProfileImageFileId", "dbo.Files", "Id");
        }
        
        public override void Down()
        {
            this.DropForeignKey("dbo.AspNetUsers", "ProfileImageFileId", "dbo.Files");
            this.DropColumn("dbo.AspNetUsers", "ProfileImageFileId");
        }
    }
}
