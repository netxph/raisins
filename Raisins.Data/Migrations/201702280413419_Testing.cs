namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Testing : DbMigration
    {
        public override void Up()
        {
            //Sql("INSERT INTO roles(name, permissions) VALUES ('Super', 'payments_create_new; payments_view_summary;" +
            //    "payments_create_new," +
            //    "payments_view_list_all;" +
            //    "beneficiaries_view;beneficiaries_create;beneficiaries_update;" +
            //    "roles_view;roles_edit;roles_create;" +
            //    "accounts_create;accounts_edit;acounts_view')");
            //Sql("INSERT INTO roles(name, permissions) VALUES ('Tester', 'Testing_Role')");
            //Sql("INSERT INTO roles(name, permissions) VALUES ('Administrator', 'payments_lock;payments_unlock;payments_create_new;payments_view_summary')");
            //Sql("INSERT INTO roles(name, permissions) VALUES ('Accountant', 'payments_view_summary')");
            //Sql("INSERT INTO roles(name, permissions) VALUES ('Manager', 'payments_lock;payments_unlock;payments_create_new;payments_view_summary')");
            //Sql("INSERT INTO roles(name, permissions) VALUES ('User', 'payments_create_new')");
            //Sql("INSERT INTO roles(name, permissions) VALUES ('SuperAccountant', 'payments_view_list_all')");
            //Sql("INSERT INTO roles(name, permissions) VALUES ('SuperAdmininstrator', 'beneficiaries_view;beneficiaries_create;beneficiaries_update')");

        }
        
        public override void Down()
        {
        }
    }
}
