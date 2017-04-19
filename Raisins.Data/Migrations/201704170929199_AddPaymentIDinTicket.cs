namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentIDinTicket : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "PaymentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "PaymentID");
            AddForeignKey("dbo.Tickets", "PaymentID", "dbo.Payments", "PaymentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "PaymentID", "dbo.Payments");
            DropIndex("dbo.Tickets", new[] { "PaymentID" });
            DropColumn("dbo.Tickets", "PaymentID");
        }
    }
}
