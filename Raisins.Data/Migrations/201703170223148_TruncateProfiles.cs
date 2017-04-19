namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TruncateProfiles : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM AccountProfiles WHERE profileID > 4");
        }
        
        public override void Down()
        {
        }
    }
}
