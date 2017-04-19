namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProfiles : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM accounts where accountID > 4");
        }
        
        public override void Down()
        {
        }
    }
}
