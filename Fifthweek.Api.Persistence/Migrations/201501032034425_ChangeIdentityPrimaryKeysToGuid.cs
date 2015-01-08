using System.Data.Entity.Migrations;

namespace Fifthweek.Api.Persistence.Migrations
{
    public partial class ChangeIdentityPrimaryKeysToGuid : DbMigration
    {
        public override void Up()
        {
            // The migrations below are not necessary if your identity tables already have
            // GUID primary keys, which they should if you ran the latest migrations from scratch as they
            // have been updated from InitialCreate to use GUIDs.
            // If your PKs are strings, you need to run the migration below if running locally,
            // or the .SQL file by the same name in Azure (as Azure doesn't support removing a clustered
            // index from a table).
        }

        public override void Down()
        {
        }

        public void GeneratedUp()
        {
            // These don't work on Azure as you can't have a table with no clustered index.
            this.DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            this.DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            this.DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            this.DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            this.DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            this.DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            this.DropPrimaryKey("dbo.AspNetRoles");
            this.DropPrimaryKey("dbo.AspNetUserRoles");
            this.DropPrimaryKey("dbo.AspNetUsers");
            this.DropPrimaryKey("dbo.AspNetUserLogins");
            this.AlterColumn("dbo.AspNetRoles", "Id", c => c.Guid(nullable: false));
            this.AlterColumn("dbo.AspNetUserRoles", "UserId", c => c.Guid(nullable: false));
            this.AlterColumn("dbo.AspNetUserRoles", "RoleId", c => c.Guid(nullable: false));
            this.AlterColumn("dbo.AspNetUsers", "Id", c => c.Guid(nullable: false));
            this.AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.Guid(nullable: false));
            this.AlterColumn("dbo.AspNetUserLogins", "UserId", c => c.Guid(nullable: false));
            this.AddPrimaryKey("dbo.AspNetRoles", "Id");
            this.AddPrimaryKey("dbo.AspNetUserRoles", new[] { "UserId", "RoleId" });
            this.AddPrimaryKey("dbo.AspNetUsers", "Id");
            this.AddPrimaryKey("dbo.AspNetUserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            this.CreateIndex("dbo.AspNetUserRoles", "UserId");
            this.CreateIndex("dbo.AspNetUserRoles", "RoleId");
            this.CreateIndex("dbo.AspNetUserClaims", "UserId");
            this.CreateIndex("dbo.AspNetUserLogins", "UserId");
            this.AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }

        public void GeneratedDown()
        {
            // These don't work on Azure as you can't have a table with no clustered index.
            this.DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            this.DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            this.DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            this.DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            this.DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            this.DropPrimaryKey("dbo.AspNetUserLogins");
            this.DropPrimaryKey("dbo.AspNetUsers");
            this.DropPrimaryKey("dbo.AspNetUserRoles");
            this.DropPrimaryKey("dbo.AspNetRoles");
            this.AlterColumn("dbo.AspNetUserLogins", "UserId", c => c.String(nullable: false, maxLength: 128));
            this.AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            this.AlterColumn("dbo.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            this.AlterColumn("dbo.AspNetUserRoles", "RoleId", c => c.String(nullable: false, maxLength: 128));
            this.AlterColumn("dbo.AspNetUserRoles", "UserId", c => c.String(nullable: false, maxLength: 128));
            this.AlterColumn("dbo.AspNetRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            this.AddPrimaryKey("dbo.AspNetUserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            this.AddPrimaryKey("dbo.AspNetUsers", "Id");
            this.AddPrimaryKey("dbo.AspNetUserRoles", new[] { "UserId", "RoleId" });
            this.AddPrimaryKey("dbo.AspNetRoles", "Id");
            this.CreateIndex("dbo.AspNetUserLogins", "UserId");
            this.CreateIndex("dbo.AspNetUserClaims", "UserId");
            this.CreateIndex("dbo.AspNetUserRoles", "RoleId");
            this.CreateIndex("dbo.AspNetUserRoles", "UserId");
            this.AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            this.AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
