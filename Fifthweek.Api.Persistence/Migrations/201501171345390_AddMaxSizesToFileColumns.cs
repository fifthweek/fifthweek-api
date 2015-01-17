namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxSizesToFileColumns : DbMigration
    {
        public override void Up()
        {
            const int MaxFileNameWithoutExtension = 255;
            const int MaxFileExtension = 10;

            Sql(string.Format("UPDATE [dbo].[Files] SET {0}=LEFT({0}, {1})", "FileNameWithoutExtension", MaxFileNameWithoutExtension));
            Sql(string.Format("UPDATE [dbo].[Files] SET {0}=LEFT({0}, {1})", "FileExtension", MaxFileExtension));

            AlterColumn("dbo.Files", "FileNameWithoutExtension", c => c.String(nullable: false, maxLength: MaxFileNameWithoutExtension));
            AlterColumn("dbo.Files", "FileExtension", c => c.String(nullable: false, maxLength: MaxFileExtension));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "FileExtension", c => c.String(nullable: false));
            AlterColumn("dbo.Files", "FileNameWithoutExtension", c => c.String(nullable: false));
        }
    }
}
