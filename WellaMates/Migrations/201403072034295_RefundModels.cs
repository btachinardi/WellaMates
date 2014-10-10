namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Freelancer", "ManagerID", "dbo.Manager");
            DropForeignKey("dbo.RefundItemUpdate", "RefundProfile_UserID", "dbo.RefundProfile");
            DropIndex("dbo.Freelancer", new[] { "ManagerID" });
            DropIndex("dbo.RefundItemUpdate", new[] { "RefundProfile_UserID" });
            DropColumn("dbo.RefundItemUpdate", "RefundProfileID");
            RenameColumn(table: "dbo.RefundItemUpdate", name: "RefundProfile_UserID", newName: "RefundProfileID");
            CreateTable(
                "dbo.File",
                c => new
                    {
                        FileID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        RefundItem_RefundItemID = c.Int(),
                    })
                .PrimaryKey(t => t.FileID)
                .ForeignKey("dbo.RefundItem", t => t.RefundItem_RefundItemID)
                .Index(t => t.RefundItem_RefundItemID);
            
            CreateTable(
                "dbo.Refund",
                c => new
                    {
                        RefundID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        FreelancerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RefundID)
                .ForeignKey("dbo.Freelancer", t => t.FreelancerID, cascadeDelete: true)
                .Index(t => t.FreelancerID);
            
            CreateTable(
                "dbo.RefundItem",
                c => new
                    {
                        RefundItemID = c.Int(nullable: false, identity: true),
                        Category = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RefundID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RefundItemID)
                .ForeignKey("dbo.Refund", t => t.RefundID, cascadeDelete: true)
                .Index(t => t.RefundID);
            
            CreateTable(
                "dbo.ManagerFreelancer",
                c => new
                    {
                        Manager_UserID = c.Int(nullable: false),
                        Freelancer_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Manager_UserID, t.Freelancer_UserID })
                .ForeignKey("dbo.Manager", t => t.Manager_UserID, cascadeDelete: true)
                .ForeignKey("dbo.Freelancer", t => t.Freelancer_UserID, cascadeDelete: true)
                .Index(t => t.Manager_UserID)
                .Index(t => t.Freelancer_UserID);
            
            AddColumn("dbo.RefundItemUpdate", "Comment", c => c.String());
            AddColumn("dbo.RefundItemUpdate", "Changelog", c => c.String());
            AlterColumn("dbo.RefundItemUpdate", "RefundItemUpdateID", c => c.Int(nullable: false));
            AlterColumn("dbo.RefundItemUpdate", "RefundProfileID", c => c.Int(nullable: false));
            CreateIndex("dbo.RefundItemUpdate", "RefundItemUpdateID");
            CreateIndex("dbo.RefundItemUpdate", "RefundProfileID");
            AddForeignKey("dbo.RefundItemUpdate", "RefundItemUpdateID", "dbo.RefundItem", "RefundItemID");
            AddForeignKey("dbo.RefundItemUpdate", "RefundProfileID", "dbo.RefundProfile", "UserID", cascadeDelete: true);
            DropColumn("dbo.Freelancer", "ManagerID");
            DropColumn("dbo.RefundItemUpdate", "Comments");
            DropColumn("dbo.RefundItemUpdate", "ImageUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefundItemUpdate", "ImageUrl", c => c.String());
            AddColumn("dbo.RefundItemUpdate", "Comments", c => c.String());
            AddColumn("dbo.Freelancer", "ManagerID", c => c.Int(nullable: false));
            DropForeignKey("dbo.RefundItemUpdate", "RefundProfileID", "dbo.RefundProfile");
            DropForeignKey("dbo.RefundItem", "RefundID", "dbo.Refund");
            DropForeignKey("dbo.RefundItemUpdate", "RefundItemUpdateID", "dbo.RefundItem");
            DropForeignKey("dbo.File", "RefundItem_RefundItemID", "dbo.RefundItem");
            DropForeignKey("dbo.Refund", "FreelancerID", "dbo.Freelancer");
            DropForeignKey("dbo.ManagerFreelancer", "Freelancer_UserID", "dbo.Freelancer");
            DropForeignKey("dbo.ManagerFreelancer", "Manager_UserID", "dbo.Manager");
            DropIndex("dbo.RefundItemUpdate", new[] { "RefundProfileID" });
            DropIndex("dbo.RefundItem", new[] { "RefundID" });
            DropIndex("dbo.RefundItemUpdate", new[] { "RefundItemUpdateID" });
            DropIndex("dbo.File", new[] { "RefundItem_RefundItemID" });
            DropIndex("dbo.Refund", new[] { "FreelancerID" });
            DropIndex("dbo.ManagerFreelancer", new[] { "Freelancer_UserID" });
            DropIndex("dbo.ManagerFreelancer", new[] { "Manager_UserID" });
            AlterColumn("dbo.RefundItemUpdate", "RefundProfileID", c => c.Int());
            AlterColumn("dbo.RefundItemUpdate", "RefundItemUpdateID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.RefundItemUpdate", "Changelog");
            DropColumn("dbo.RefundItemUpdate", "Comment");
            DropTable("dbo.ManagerFreelancer");
            DropTable("dbo.RefundItem");
            DropTable("dbo.Refund");
            DropTable("dbo.File");
            RenameColumn(table: "dbo.RefundItemUpdate", name: "RefundProfileID", newName: "RefundProfile_UserID");
            AddColumn("dbo.RefundItemUpdate", "RefundProfileID", c => c.Int(nullable: false));
            CreateIndex("dbo.RefundItemUpdate", "RefundProfile_UserID");
            CreateIndex("dbo.Freelancer", "ManagerID");
            AddForeignKey("dbo.RefundItemUpdate", "RefundProfile_UserID", "dbo.RefundProfile", "UserID");
            AddForeignKey("dbo.Freelancer", "ManagerID", "dbo.Manager", "UserID", cascadeDelete: true);
        }
    }
}
