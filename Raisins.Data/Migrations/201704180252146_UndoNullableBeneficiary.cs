namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UndoNullableBeneficiary : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM payments where paymentID > 0");
            Sql("TRUNCATE TABLE Tickets");
            Sql("TRUNCATE TABLE MailQueues");
            Sql("UPDATE payments SET locked = 'false' where paymentID > 0");
            DropForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries");
            DropIndex("dbo.Payments", new[] { "BeneficiaryID" });
            AlterColumn("dbo.Payments", "BeneficiaryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "BeneficiaryID");
            AddForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries", "BeneficiaryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries");
            DropIndex("dbo.Payments", new[] { "BeneficiaryID" });
            AlterColumn("dbo.Payments", "BeneficiaryID", c => c.Int());
            CreateIndex("dbo.Payments", "BeneficiaryID");
            AddForeignKey("dbo.Payments", "BeneficiaryID", "dbo.Beneficiaries", "BeneficiaryID");
        }
    }
}
