namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToFilesOnCreatedDateAndAddAllTestUsersToTestUserRole : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Files", "UploadStartedDate");
            Sql(@"INSERT INTO AspNetUserRoles 
                    SELECT u.Id AS UserId, '1E68420E-6388-4A41-B4EF-459A27811D31' AS RoleId  
                    FROM AspNetUsers u 
                        WHERE u.Email LIKE '%@testing.fifthweek.com' 
                        AND NOT EXISTS (SELECT * FROM AspNetUserRoles ur WHERE ur.UserId=u.Id AND ur.RoleId='1E68420E-6388-4A41-B4EF-459A27811D31')");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Files", new[] { "UploadStartedDate" });
        }
    }
}
