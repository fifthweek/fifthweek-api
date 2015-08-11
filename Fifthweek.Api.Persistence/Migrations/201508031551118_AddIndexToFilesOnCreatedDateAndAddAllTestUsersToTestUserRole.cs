namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToFilesOnCreatedDateAndAddAllTestUsersToTestUserRole : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Files", "UploadStartedDate");
            Sql(@"INSERT INTO AspNetRoles VALUES ('1E68420E-6388-4A41-B4EF-459A27811D31', 'test-user')");
            Sql(@"INSERT INTO AspNetUserRoles 
                    SELECT u.Id AS UserId, '1E68420E-6388-4A41-B4EF-459A27811D31' AS RoleId  
                    FROM AspNetUsers u 
                        WHERE u.Email LIKE '%@testing.fifthweek.com'");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Files", new[] { "UploadStartedDate" });
        }
    }
}
