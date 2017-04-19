namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reformat : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE Tickets");
            Sql("TRUNCATE TABLE MailQueues");
            Sql("UPDATE payments SET locked = 'false' where paymentID > 0");
        }
        
        public override void Down()
        {
        }
    }
}
