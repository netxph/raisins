namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteDataFromTickets : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE Tickets");
        }
        
        public override void Down()
        {
        }
    }
}
