namespace AssetTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeProlertiesName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganizationBranches", "OrganizationBranchCode", c => c.String());
            DropColumn("dbo.OrganizationBranches", "OrganizationCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrganizationBranches", "OrganizationCode", c => c.String());
            DropColumn("dbo.OrganizationBranches", "OrganizationBranchCode");
        }
    }
}
