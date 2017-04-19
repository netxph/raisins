namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPayments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Beneficiaries",
                c => new
                    {
                        BeneficiaryID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.BeneficiaryID);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        CurrencyID = c.Int(nullable: false, identity: true),
                        CurrencyCode = c.String(nullable: false),
                        Ratio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExchangeRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CurrencyID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Locked = c.Boolean(nullable: false),
                        BeneficiaryID = c.Int(nullable: false),
                        CurrencyID = c.Int(nullable: false),
                        CreatedByID = c.Int(nullable: false),
                        CreatedBy_AccountID = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Beneficiaries", t => t.BeneficiaryID, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.CreatedBy_AccountID)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID, cascadeDelete: true)
                .Index(t => t.BeneficiaryID)
                .Index(t => t.CurrencyID)
                .Index(t => t.CreatedBy_AccountID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.Payments", "CreatedBy_AccountID", "dbo.Accounts");
            DropForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries");
            DropIndex("dbo.Payments", new[] { "CreatedBy_AccountID" });
            DropIndex("dbo.Payments", new[] { "CurrencyID" });
            DropIndex("dbo.Payments", new[] { "BeneficiaryID" });
            DropTable("dbo.Payments");
            DropTable("dbo.Currencies");
            DropTable("dbo.Beneficiaries");
        }
    }
}
