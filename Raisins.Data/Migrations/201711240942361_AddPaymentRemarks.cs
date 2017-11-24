namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentRemarks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "Remarks");
        }
    }
}
