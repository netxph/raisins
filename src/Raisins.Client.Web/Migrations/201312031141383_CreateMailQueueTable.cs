namespace Raisins.Client.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateMailQueueTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MailQueues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        From = c.String(nullable: false),
                        To = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MailQueues");
        }
    }
}
