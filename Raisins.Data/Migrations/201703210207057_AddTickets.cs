namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTickets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Long(nullable: false, identity: true),
                        TicketCode = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TicketID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tickets");
        }
    }
}
