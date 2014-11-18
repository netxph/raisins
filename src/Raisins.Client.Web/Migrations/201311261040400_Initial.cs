namespace Raisins.Client.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        AccountProfileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccountProfiles", t => t.AccountProfileID, cascadeDelete: true)
                .Index(t => t.AccountProfileID);
            
            CreateTable(
                "dbo.AccountProfiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsLocal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Beneficiaries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CurrencyCode = c.String(nullable: false),
                        Ratio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Executives",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Location = c.String(),
                        Email = c.String(nullable: false),
                        SoldBy = c.String(nullable: true),
                        Remarks = c.String(),
                        ClassID = c.Int(nullable: false),
                        Locked = c.Boolean(nullable: false),
                        BeneficiaryID = c.Int(nullable: false),
                        ExecutiveID = c.Int(),
                        CurrencyID = c.Int(nullable: false),
                        CreatedByID = c.Int(nullable: false),
                        AuditedByID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.AuditedByID)
                .ForeignKey("dbo.Beneficiaries", t => t.BeneficiaryID, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.CreatedByID, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("dbo.Executives", t => t.ExecutiveID)
                .Index(t => t.AuditedByID)
                .Index(t => t.BeneficiaryID)
                .Index(t => t.CreatedByID)
                .Index(t => t.CurrencyID)
                .Index(t => t.ExecutiveID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TicketCode = c.String(),
                        Name = c.String(),
                        Payment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Payments", t => t.Payment_ID)
                .Index(t => t.Payment_ID);
            
            CreateTable(
                "dbo.AccountProfileBeneficiaries",
                c => new
                    {
                        AccountProfile_ID = c.Int(nullable: false),
                        Beneficiary_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountProfile_ID, t.Beneficiary_ID })
                .ForeignKey("dbo.AccountProfiles", t => t.AccountProfile_ID, cascadeDelete: true)
                .ForeignKey("dbo.Beneficiaries", t => t.Beneficiary_ID, cascadeDelete: true)
                .Index(t => t.AccountProfile_ID)
                .Index(t => t.Beneficiary_ID);
            
            CreateTable(
                "dbo.AccountProfileCurrencies",
                c => new
                    {
                        AccountProfile_ID = c.Int(nullable: false),
                        Currency_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountProfile_ID, t.Currency_ID })
                .ForeignKey("dbo.AccountProfiles", t => t.AccountProfile_ID, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.Currency_ID, cascadeDelete: true)
                .Index(t => t.AccountProfile_ID)
                .Index(t => t.Currency_ID);
            
            CreateTable(
                "dbo.AccountRoles",
                c => new
                    {
                        Account_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Account_ID, t.Role_ID })
                .ForeignKey("dbo.Accounts", t => t.Account_ID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.Account_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.ActivityRoles",
                c => new
                    {
                        Activity_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Activity_ID, t.Role_ID })
                .ForeignKey("dbo.Activities", t => t.Activity_ID, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.Activity_ID)
                .Index(t => t.Role_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "Payment_ID", "dbo.Payments");
            DropForeignKey("dbo.Payments", "ExecutiveID", "dbo.Executives");
            DropForeignKey("dbo.Payments", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.Payments", "CreatedByID", "dbo.Accounts");
            DropForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries");
            DropForeignKey("dbo.Payments", "AuditedByID", "dbo.Accounts");
            DropForeignKey("dbo.ActivityRoles", "Role_ID", "dbo.Roles");
            DropForeignKey("dbo.ActivityRoles", "Activity_ID", "dbo.Activities");
            DropForeignKey("dbo.AccountRoles", "Role_ID", "dbo.Roles");
            DropForeignKey("dbo.AccountRoles", "Account_ID", "dbo.Accounts");
            DropForeignKey("dbo.Accounts", "AccountProfileID", "dbo.AccountProfiles");
            DropForeignKey("dbo.AccountProfileCurrencies", "Currency_ID", "dbo.Currencies");
            DropForeignKey("dbo.AccountProfileCurrencies", "AccountProfile_ID", "dbo.AccountProfiles");
            DropForeignKey("dbo.AccountProfileBeneficiaries", "Beneficiary_ID", "dbo.Beneficiaries");
            DropForeignKey("dbo.AccountProfileBeneficiaries", "AccountProfile_ID", "dbo.AccountProfiles");
            DropIndex("dbo.Tickets", new[] { "Payment_ID" });
            DropIndex("dbo.Payments", new[] { "ExecutiveID" });
            DropIndex("dbo.Payments", new[] { "CurrencyID" });
            DropIndex("dbo.Payments", new[] { "CreatedByID" });
            DropIndex("dbo.Payments", new[] { "BeneficiaryID" });
            DropIndex("dbo.Payments", new[] { "AuditedByID" });
            DropIndex("dbo.ActivityRoles", new[] { "Role_ID" });
            DropIndex("dbo.ActivityRoles", new[] { "Activity_ID" });
            DropIndex("dbo.AccountRoles", new[] { "Role_ID" });
            DropIndex("dbo.AccountRoles", new[] { "Account_ID" });
            DropIndex("dbo.Accounts", new[] { "AccountProfileID" });
            DropIndex("dbo.AccountProfileCurrencies", new[] { "Currency_ID" });
            DropIndex("dbo.AccountProfileCurrencies", new[] { "AccountProfile_ID" });
            DropIndex("dbo.AccountProfileBeneficiaries", new[] { "Beneficiary_ID" });
            DropIndex("dbo.AccountProfileBeneficiaries", new[] { "AccountProfile_ID" });
            DropTable("dbo.ActivityRoles");
            DropTable("dbo.AccountRoles");
            DropTable("dbo.AccountProfileCurrencies");
            DropTable("dbo.AccountProfileBeneficiaries");
            DropTable("dbo.Tickets");
            DropTable("dbo.Payments");
            DropTable("dbo.Executives");
            DropTable("dbo.Activities");
            DropTable("dbo.Roles");
            DropTable("dbo.Currencies");
            DropTable("dbo.Beneficiaries");
            DropTable("dbo.AccountProfiles");
            DropTable("dbo.Accounts");
        }
    }
}
