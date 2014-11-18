namespace Raisins.Client.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "SoldBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "SoldBy");
        }
    }
}
