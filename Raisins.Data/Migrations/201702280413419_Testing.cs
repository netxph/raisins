namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Testing : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO roles(name, permissions) VALUES ('Tester', 'Testing_Role')");
        }
        
        public override void Down()
        {
        }
    }
}
