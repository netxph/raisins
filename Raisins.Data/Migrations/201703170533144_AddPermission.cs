namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermission : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE roles SET permissions = 'payments_view_summary;payments_view_list;payments_create_new'  where name = 'Accountant'");
        }
        
        public override void Down()
        {
        }
    }
}
