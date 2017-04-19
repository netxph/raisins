namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketPermissionAndPublish : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE roles SET permissions = 'payments_view_summary;payments_view_list;payments_create_new;payments_publish;tickets_create'  where name = 'Accountant'");
            Sql("UPDATE roles SET permissions = 'payments_view_list_all;payments_create_new;payments_publish;tickets_create'  where name = 'SuperAccountant'");
        }
        
        public override void Down()
        {
        }
    }
}
