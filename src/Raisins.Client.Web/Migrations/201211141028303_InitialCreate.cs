namespace Raisins.Client.Web.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        Salt = c.String(),
                        AccountProfileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("AccountProfiles", t => t.AccountProfileID, cascadeDelete: true)
                .Index(t => t.AccountProfileID);
            
            CreateTable(
                "Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "AccountProfiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsLocal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Beneficiaries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Currencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CurrencyCode = c.String(nullable: false),
                        Ratio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Payments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Location = c.String(),
                        Email = c.String(),
                        Remarks = c.String(),
                        ClassID = c.Int(nullable: false),
                        Locked = c.Boolean(nullable: false),
                        BeneficiaryID = c.Int(nullable: false),
                        ExecutiveID = c.Int(),
                        CurrencyID = c.Int(nullable: false),
                        CreatedByID = c.Int(nullable: false),
                        AuditedByID = c.Int(),
                        //starts here
                        //CreatedDate=c.DateTime(nullable:false),
                        //ModifiedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Beneficiaries", t => t.BeneficiaryID, cascadeDelete: true)
                .ForeignKey("Executives", t => t.ExecutiveID)
                .ForeignKey("Currencies", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("Accounts", t => t.CreatedByID, cascadeDelete: true)
                .ForeignKey("Accounts", t => t.AuditedByID)
                .Index(t => t.BeneficiaryID)
                .Index(t => t.ExecutiveID)
                .Index(t => t.CurrencyID)
                .Index(t => t.CreatedByID)
                .Index(t => t.AuditedByID);
                //startshere
                //.Index(t => t.CreatedDate)
                //.Index(t => t.ModifiedDate);
            
            CreateTable(
                "Tickets",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        TicketCode = c.String(),
                        Name = c.String(),
                        Payment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Payments", t => t.Payment_ID)
                .Index(t => t.Payment_ID);
            
            CreateTable(
                "Executives",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "Activities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "AccountRoles",
                c => new
                    {
                        Account_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Account_ID, t.Role_ID })
                .ForeignKey("Accounts", t => t.Account_ID, cascadeDelete: true)
                .ForeignKey("Roles", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.Account_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "AccountProfileBeneficiaries",
                c => new
                    {
                        AccountProfile_ID = c.Int(nullable: false),
                        Beneficiary_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountProfile_ID, t.Beneficiary_ID })
                .ForeignKey("AccountProfiles", t => t.AccountProfile_ID, cascadeDelete: true)
                .ForeignKey("Beneficiaries", t => t.Beneficiary_ID, cascadeDelete: true)
                .Index(t => t.AccountProfile_ID)
                .Index(t => t.Beneficiary_ID);
            
            CreateTable(
                "AccountProfileCurrencies",
                c => new
                    {
                        AccountProfile_ID = c.Int(nullable: false),
                        Currency_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountProfile_ID, t.Currency_ID })
                .ForeignKey("AccountProfiles", t => t.AccountProfile_ID, cascadeDelete: true)
                .ForeignKey("Currencies", t => t.Currency_ID, cascadeDelete: true)
                .Index(t => t.AccountProfile_ID)
                .Index(t => t.Currency_ID);
            
            CreateTable(
                "ActivityRoles",
                c => new
                    {
                        Activity_ID = c.Int(nullable: false),
                        Role_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Activity_ID, t.Role_ID })
                .ForeignKey("Activities", t => t.Activity_ID, cascadeDelete: true)
                .ForeignKey("Roles", t => t.Role_ID, cascadeDelete: true)
                .Index(t => t.Activity_ID)
                .Index(t => t.Role_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("ActivityRoles", new[] { "Role_ID" });
            DropIndex("ActivityRoles", new[] { "Activity_ID" });
            DropIndex("AccountProfileCurrencies", new[] { "Currency_ID" });
            DropIndex("AccountProfileCurrencies", new[] { "AccountProfile_ID" });
            DropIndex("AccountProfileBeneficiaries", new[] { "Beneficiary_ID" });
            DropIndex("AccountProfileBeneficiaries", new[] { "AccountProfile_ID" });
            DropIndex("AccountRoles", new[] { "Role_ID" });
            DropIndex("AccountRoles", new[] { "Account_ID" });
            DropIndex("Tickets", new[] { "Payment_ID" });
            DropIndex("Payments", new[] { "AuditedByID" });
            DropIndex("Payments", new[] { "CreatedByID" });
            DropIndex("Payments", new[] { "CurrencyID" });
            DropIndex("Payments", new[] { "ExecutiveID" });
            DropIndex("Payments", new[] { "BeneficiaryID" });
            DropIndex("Accounts", new[] { "AccountProfileID" });
            DropForeignKey("ActivityRoles", "Role_ID", "Roles");
            DropForeignKey("ActivityRoles", "Activity_ID", "Activities");
            DropForeignKey("AccountProfileCurrencies", "Currency_ID", "Currencies");
            DropForeignKey("AccountProfileCurrencies", "AccountProfile_ID", "AccountProfiles");
            DropForeignKey("AccountProfileBeneficiaries", "Beneficiary_ID", "Beneficiaries");
            DropForeignKey("AccountProfileBeneficiaries", "AccountProfile_ID", "AccountProfiles");
            DropForeignKey("AccountRoles", "Role_ID", "Roles");
            DropForeignKey("AccountRoles", "Account_ID", "Accounts");
            DropForeignKey("Tickets", "Payment_ID", "Payments");
            DropForeignKey("Payments", "AuditedByID", "Accounts");
            DropForeignKey("Payments", "CreatedByID", "Accounts");
            DropForeignKey("Payments", "CurrencyID", "Currencies");
            DropForeignKey("Payments", "ExecutiveID", "Executives");
            DropForeignKey("Payments", "BeneficiaryID", "Beneficiaries");
            DropForeignKey("Accounts", "AccountProfileID", "AccountProfiles");
            DropTable("ActivityRoles");
            DropTable("AccountProfileCurrencies");
            DropTable("AccountProfileBeneficiaries");
            DropTable("AccountRoles");
            DropTable("Activities");
            DropTable("Executives");
            DropTable("Tickets");
            DropTable("Payments");
            DropTable("Currencies");
            DropTable("Beneficiaries");
            DropTable("AccountProfiles");
            DropTable("Roles");
            DropTable("Accounts");
        }
    }
}
