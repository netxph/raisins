namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetTemporaryModifiedBy : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE payments SET ModifiedByID = 1  where paymentID > 0");
        }
        
        public override void Down()
        {
        }
    }
}
