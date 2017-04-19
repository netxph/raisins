namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableBeneficiary : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries");
            DropIndex("dbo.Payments", new[] { "BeneficiaryID" });
            AlterColumn("dbo.Payments", "BeneficiaryID", c => c.Int());
            CreateIndex("dbo.Payments", "BeneficiaryID");
            AddForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries", "BeneficiaryID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries");
            DropIndex("dbo.Payments", new[] { "BeneficiaryID" });
            AlterColumn("dbo.Payments", "BeneficiaryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "BeneficiaryID");
            AddForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries", "BeneficiaryID", cascadeDelete: true);
        }
    }
}
