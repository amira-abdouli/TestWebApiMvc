namespace TestWebApiMvc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateData : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleGruopJoinUsers", "UserRoleGruopID", "dbo.UserRoleGruops");
            DropForeignKey("dbo.RoleJoinRoleGruops", "UserRoleGruopID", "dbo.UserRoleGruops");
            DropForeignKey("dbo.RoleJoinRoleGruops", "UserRoleID", "dbo.UserRoles");
            DropForeignKey("dbo.RoleGruopJoinUsers", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.RoleJoinRoleGruops", new[] { "UserRoleGruopID" });
            DropIndex("dbo.RoleJoinRoleGruops", new[] { "UserRoleID" });
            DropIndex("dbo.RoleGruopJoinUsers", new[] { "UserRoleGruopID" });
            DropIndex("dbo.RoleGruopJoinUsers", new[] { "UserID" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.RoleJoinRoleGruops");
            DropTable("dbo.UserRoleGruops");
            DropTable("dbo.RoleGruopJoinUsers");
        }
    }
}
