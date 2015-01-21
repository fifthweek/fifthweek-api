namespace Fifthweek.Api.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCollectionIdIndexToPosts : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "CollectionId" });
            CreateIndex("dbo.Posts", "CollectionId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Posts", new[] { "CollectionId" });
            CreateIndex("dbo.Posts", "CollectionId");
        }
    }
}
