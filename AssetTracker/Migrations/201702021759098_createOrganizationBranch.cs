namespace AssetTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createOrganizationBranch : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrganizationBranches", "OrganizationBranchName", c => c.String(nullable: false));
            DropColumn("dbo.OrganizationBranches", "OrganizationBranches");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrganizationBranches", "OrganizationBranches", c => c.String(nullable: false));
            DropColumn("dbo.OrganizationBranches", "OrganizationBranchName");
        }
    }
}
