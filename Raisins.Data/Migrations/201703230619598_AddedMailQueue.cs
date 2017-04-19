namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMailQueue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailQueues",
                c => new
                    {
                        MailQueueID = c.Int(nullable: false, identity: true),
                        PaymentID = c.Int(nullable: false),
                        Status = c.String(),
                        From = c.String(),
                        To = c.String(),
                        Subject = c.String(),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.MailQueueID);

            Sql("TRUNCATE TABLE Tickets");
            Sql("UPDATE payments SET locked = 'false' where paymentID > 0");

        }
        
        public override void Down()
        {
            DropTable("dbo.MailQueues");
        }
    }
}
