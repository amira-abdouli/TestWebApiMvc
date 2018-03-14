namespace BLL1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorys",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        IDCategory = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categorys", t => t.IDCategory, cascadeDelete: true)
                .Index(t => t.IDCategory);
            
            CreateTable(
                "dbo.RoleGruopJoinUsers",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        UserRoleGruopID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.UserRoleGruopID })
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.UserRoleGruops", t => t.UserRoleGruopID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.UserRoleGruopID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoleGruops",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleJoinRoleGruops",
                c => new
                    {
                        UserRoleID = c.Guid(nullable: false),
                        UserRoleGruopID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRoleID, t.UserRoleGruopID })
                .ForeignKey("dbo.UserRoles", t => t.UserRoleID, cascadeDelete: true)
                .ForeignKey("dbo.UserRoleGruops", t => t.UserRoleGruopID, cascadeDelete: true)
                .Index(t => t.UserRoleID)
                .Index(t => t.UserRoleGruopID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.RoleGruopJoinUsers", "UserRoleGruopID", "dbo.UserRoleGruops");
            DropForeignKey("dbo.RoleJoinRoleGruops", "UserRoleGruopID", "dbo.UserRoleGruops");
            DropForeignKey("dbo.RoleJoinRoleGruops", "UserRoleID", "dbo.UserRoles");
            DropForeignKey("dbo.RoleGruopJoinUsers", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Products", "IDCategory", "dbo.Categorys");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.RoleJoinRoleGruops", new[] { "UserRoleGruopID" });
            DropIndex("dbo.RoleJoinRoleGruops", new[] { "UserRoleID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.RoleGruopJoinUsers", new[] { "UserRoleGruopID" });
            DropIndex("dbo.RoleGruopJoinUsers", new[] { "UserID" });
            DropIndex("dbo.Products", new[] { "IDCategory" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.RoleJoinRoleGruops");
            DropTable("dbo.UserRoleGruops");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.RoleGruopJoinUsers");
            DropTable("dbo.Products");
            DropTable("dbo.Categorys");
        }
    }
}
