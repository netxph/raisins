namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletePayments : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM payments where paymentID > 10");
        }
        
        public override void Down()
        {
        }
    }
}
