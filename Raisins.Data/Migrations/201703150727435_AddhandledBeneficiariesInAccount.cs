namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddhandledBeneficiariesInAccount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountProfileBeneficiaries",
                c => new
                    {
                        AccountProfile_ProfileID = c.Int(nullable: false),
                        Beneficiary_BeneficiaryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AccountProfile_ProfileID, t.Beneficiary_BeneficiaryID })
                .ForeignKey("dbo.AccountProfiles", t => t.AccountProfile_ProfileID, cascadeDelete: true)
                .ForeignKey("dbo.Beneficiaries", t => t.Beneficiary_BeneficiaryID, cascadeDelete: true)
                .Index(t => t.AccountProfile_ProfileID)
                .Index(t => t.Beneficiary_BeneficiaryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountProfileBeneficiaries", "Beneficiary_BeneficiaryID", "dbo.Beneficiaries");
            DropForeignKey("dbo.AccountProfileBeneficiaries", "AccountProfile_ProfileID", "dbo.AccountProfiles");
            DropIndex("dbo.AccountProfileBeneficiaries", new[] { "Beneficiary_BeneficiaryID" });
            DropIndex("dbo.AccountProfileBeneficiaries", new[] { "AccountProfile_ProfileID" });
            DropTable("dbo.AccountProfileBeneficiaries");
        }
    }
}
