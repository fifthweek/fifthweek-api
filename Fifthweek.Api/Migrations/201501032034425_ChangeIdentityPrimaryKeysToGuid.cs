namespace Fifthweek.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropPrimaryKey("dbo.AspNetRoles");
            DropPrimaryKey("dbo.AspNetUserRoles");
            DropPrimaryKey("dbo.AspNetUsers");
            DropPrimaryKey("dbo.AspNetUserLogins");
            AlterColumn("dbo.AspNetRoles", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.AspNetUserRoles", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.AspNetUserRoles", "RoleId", c => c.Guid(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.AspNetUserLogins", "UserId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.AspNetRoles", "Id");
            AddPrimaryKey("dbo.AspNetUserRoles", new[] { "UserId", "RoleId" });
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            AddPrimaryKey("dbo.AspNetUserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            CreateIndex("dbo.AspNetUserRoles", "RoleId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }

        public void GeneratedDown()
        {
            // These don't work on Azure as you can't have a table with no clustered index.
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropPrimaryKey("dbo.AspNetUserLogins");
            DropPrimaryKey("dbo.AspNetUsers");
            DropPrimaryKey("dbo.AspNetUserRoles");
            DropPrimaryKey("dbo.AspNetRoles");
            AlterColumn("dbo.AspNetUserLogins", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUserRoles", "RoleId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUserRoles", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.AspNetUserLogins", new[] { "LoginProvider", "ProviderKey", "UserId" });
            AddPrimaryKey("dbo.AspNetUsers", "Id");
            AddPrimaryKey("dbo.AspNetUserRoles", new[] { "UserId", "RoleId" });
            AddPrimaryKey("dbo.AspNetRoles", "Id");
            CreateIndex("dbo.AspNetUserLogins", "UserId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.AspNetUserRoles", "RoleId");
            CreateIndex("dbo.AspNetUserRoles", "UserId");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles", "Id", cascadeDelete: true);
        }
    }
}
