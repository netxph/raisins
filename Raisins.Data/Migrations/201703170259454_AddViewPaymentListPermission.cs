namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewPaymentListPermission : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE roles SET permissions = 'payments_view_summary;payments_view_list'  where name = 'Accountant'");
        }
        
        public override void Down()
        {
        }
    }
}
