namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyPayments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentSources",
                c => new
                    {
                        PaymentSourceID = c.Int(nullable: false, identity: true),
                        Source = c.String(),
                    })
                .PrimaryKey(t => t.PaymentSourceID);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        PaymentTypeID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.PaymentTypeID);
            
            AddColumn("dbo.Payments", "Email", c => c.String());
            AddColumn("dbo.Payments", "OptOut", c => c.Boolean(nullable: false));
            AddColumn("dbo.Payments", "ModifiedByID", c => c.Int(nullable: true));
            AddColumn("dbo.Payments", "CreatedDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.Payments", "ModifiedDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.Payments", "PaymentDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.Payments", "LockDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.Payments", "PublishDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.Payments", "PaymentSourceID", c => c.Int(nullable: true));
            AddColumn("dbo.Payments", "PaymentTypeID", c => c.Int(nullable: true));
            AddColumn("dbo.Payments", "ModifiedBy_AccountID", c => c.Int());
            CreateIndex("dbo.Payments", "PaymentSourceID");
            CreateIndex("dbo.Payments", "PaymentTypeID");
            CreateIndex("dbo.Payments", "ModifiedBy_AccountID");
            AddForeignKey("dbo.Payments", "ModifiedBy_AccountID", "dbo.Accounts", "AccountID");
            AddForeignKey("dbo.Payments", "PaymentSourceID", "dbo.PaymentSources", "PaymentSourceID", cascadeDelete: true);
            AddForeignKey("dbo.Payments", "PaymentTypeID", "dbo.PaymentTypes", "PaymentTypeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "PaymentTypeID", "dbo.PaymentTypes");
            DropForeignKey("dbo.Payments", "PaymentSourceID", "dbo.PaymentSources");
            DropForeignKey("dbo.Payments", "ModifiedBy_AccountID", "dbo.Accounts");
            DropIndex("dbo.Payments", new[] { "ModifiedBy_AccountID" });
            DropIndex("dbo.Payments", new[] { "PaymentTypeID" });
            DropIndex("dbo.Payments", new[] { "PaymentSourceID" });
            DropColumn("dbo.Payments", "ModifiedBy_AccountID");
            DropColumn("dbo.Payments", "PaymentTypeID");
            DropColumn("dbo.Payments", "PaymentSourceID");
            DropColumn("dbo.Payments", "PublishDate");
            DropColumn("dbo.Payments", "LockDate");
            DropColumn("dbo.Payments", "PaymentDate");
            DropColumn("dbo.Payments", "ModifiedDate");
            DropColumn("dbo.Payments", "CreatedDate");
            DropColumn("dbo.Payments", "ModifiedByID");
            DropColumn("dbo.Payments", "OptOut");
            DropColumn("dbo.Payments", "Email");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.PaymentSources");
        }
    }
}
