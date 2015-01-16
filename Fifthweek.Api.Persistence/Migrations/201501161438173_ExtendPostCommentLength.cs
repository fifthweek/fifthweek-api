namespace Fifthweek.Api.Persistence.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ExtendPostCommentLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Comment", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Comment", c => c.String(maxLength: 280));
        }
    }
}
