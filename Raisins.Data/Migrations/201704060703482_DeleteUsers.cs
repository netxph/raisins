namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteUsers : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM AccountProfiles WHERE ProfileID > 9");
            Sql("DELETE FROM Accounts WHERE AccountID > 9");
        }
        
        public override void Down()
        {
        }
    }
}
