namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSuperAdminAndEditRoles : DbMigration
    {
        public override void Up()
        {
            Sql("TRUNCATE TABLE Tickets");
            Sql("TRUNCATE TABLE MailQueues");
            Sql("UPDATE payments SET locked = 'false' where paymentID > 0");
            Sql("DELETE FROM payments where paymentID > 10");
            Sql("UPDATE roles SET permissions = 'payments_lock;payments_unlock;payments_create_new;payments_view_summary'  where name = 'Administrator'");
            Sql("UPDATE roles SET permissions = 'payments_lock;payments_unlock;payments_create_new;payments_view_summary'  where name = 'Manager'");
        }
        
        public override void Down()
        {
        }
    }
}
