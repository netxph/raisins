namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        RoleID = c.Int(nullable: false),
                        ProfileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountID)
                .ForeignKey("dbo.AccountProfiles", t => t.ProfileID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID)
                .Index(t => t.ProfileID);
            
            CreateTable(
                "dbo.AccountProfiles",
                c => new
                    {
                        ProfileID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsLocal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProfileID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Permissions = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Accounts", "ProfileID", "dbo.AccountProfiles");
            DropIndex("dbo.Accounts", new[] { "ProfileID" });
            DropIndex("dbo.Accounts", new[] { "RoleID" });
            DropTable("dbo.Roles");
            DropTable("dbo.AccountProfiles");
            DropTable("dbo.Accounts");
        }
    }
}
