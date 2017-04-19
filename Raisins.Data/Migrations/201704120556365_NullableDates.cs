namespace Raisins.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payments", "ModifiedDate", c => c.DateTime());
            AlterColumn("dbo.Payments", "PublishDate", c => c.DateTime());
            DropColumn("dbo.Payments", "LockDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "LockDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Payments", "PublishDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Payments", "ModifiedDate", c => c.DateTime(nullable: false));
        }
    }
}
