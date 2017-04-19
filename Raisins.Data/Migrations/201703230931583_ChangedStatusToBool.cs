namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedStatusToBool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MailQueues", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MailQueues", "Status", c => c.String());
        }
    }
}
