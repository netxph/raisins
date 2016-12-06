namespace Raisins.Client.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVideoLinkPropertyToBeneficiaries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Beneficiaries", "VideoLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Beneficiaries", "VideoLink");
        }
    }
}
