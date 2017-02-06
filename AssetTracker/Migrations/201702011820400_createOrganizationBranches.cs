namespace AssetTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createOrganizationBranches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrganizationBranches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrganizationBranches = c.String(nullable: false),
                        OrganizationCode = c.String(),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            AlterColumn("dbo.Organizations", "OrganizationName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrganizationBranches", "OrganizationId", "dbo.Organizations");
            DropIndex("dbo.OrganizationBranches", new[] { "OrganizationId" });
            AlterColumn("dbo.Organizations", "OrganizationName", c => c.String());
            DropTable("dbo.OrganizationBranches");
        }
    }
}
