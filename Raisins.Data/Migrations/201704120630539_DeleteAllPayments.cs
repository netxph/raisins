namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAllPayments : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM payments where paymentID > 0");
        }
        
        public override void Down()
        {
        }
    }
}
