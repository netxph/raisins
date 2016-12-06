namespace Raisins.Client.Web.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RemovedAttributesInPayment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "Name", c => c.String(nullable: false));
        }
    }
}
