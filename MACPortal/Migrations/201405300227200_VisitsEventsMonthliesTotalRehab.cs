namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VisitsEventsMonthliesTotalRehab : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Refund", "FreelancerID", "dbo.Freelancer");
            DropIndex("dbo.Refund", new[] { "FreelancerID" });
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        Comments = c.String(nullable: false),
                        FreelancerID = c.Int(nullable: false),
                        RefundID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Freelancer", t => t.FreelancerID, cascadeDelete: true)
                .ForeignKey("dbo.Refund", t => t.RefundID, cascadeDelete: true)
                .Index(t => t.FreelancerID)
                .Index(t => t.RefundID);
            
            CreateTable(
                "dbo.Monthly",
                c => new
                    {
                        MonthlyID = c.Int(nullable: false, identity: true),
                        FreelancerID = c.Int(nullable: false),
                        RefundID = c.Int(nullable: false),
                        Month = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.MonthlyID)
                .ForeignKey("dbo.Freelancer", t => t.FreelancerID, cascadeDelete: true)
                .ForeignKey("dbo.Refund", t => t.RefundID, cascadeDelete: true)
                .Index(t => t.FreelancerID)
                .Index(t => t.RefundID);
            
            CreateTable(
                "dbo.Visit",
                c => new
                    {
                        VisitID = c.Int(nullable: false, identity: true),
                        FreelancerID = c.Int(nullable: false),
                        RefundID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.VisitID)
                .ForeignKey("dbo.Freelancer", t => t.FreelancerID, cascadeDelete: true)
                .ForeignKey("dbo.Refund", t => t.RefundID, cascadeDelete: true)
                .Index(t => t.FreelancerID)
                .Index(t => t.RefundID);
            
            AddColumn("dbo.RefundItem", "ReceivedInvoice", c => c.Boolean(nullable: false));
            DropColumn("dbo.Refund", "Name");
            DropColumn("dbo.Refund", "Reason");
            DropColumn("dbo.Refund", "Results");
            DropColumn("dbo.Refund", "Date");
            DropColumn("dbo.Refund", "FreelancerID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Refund", "FreelancerID", c => c.Int(nullable: false));
            AddColumn("dbo.Refund", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Refund", "Results", c => c.String(nullable: false));
            AddColumn("dbo.Refund", "Reason", c => c.String(nullable: false));
            AddColumn("dbo.Refund", "Name", c => c.String(nullable: false, maxLength: 40));
            DropForeignKey("dbo.Event", "RefundID", "dbo.Refund");
            DropForeignKey("dbo.Visit", "RefundID", "dbo.Refund");
            DropForeignKey("dbo.Visit", "FreelancerID", "dbo.Freelancer");
            DropForeignKey("dbo.Monthly", "RefundID", "dbo.Refund");
            DropForeignKey("dbo.Monthly", "FreelancerID", "dbo.Freelancer");
            DropForeignKey("dbo.Event", "FreelancerID", "dbo.Freelancer");
            DropIndex("dbo.Event", new[] { "RefundID" });
            DropIndex("dbo.Visit", new[] { "RefundID" });
            DropIndex("dbo.Visit", new[] { "FreelancerID" });
            DropIndex("dbo.Monthly", new[] { "RefundID" });
            DropIndex("dbo.Monthly", new[] { "FreelancerID" });
            DropIndex("dbo.Event", new[] { "FreelancerID" });
            DropColumn("dbo.RefundItem", "ReceivedInvoice");
            DropTable("dbo.Visit");
            DropTable("dbo.Monthly");
            DropTable("dbo.Event");
            CreateIndex("dbo.Refund", "FreelancerID");
            AddForeignKey("dbo.Refund", "FreelancerID", "dbo.Freelancer", "UserID", cascadeDelete: true);
        }
    }
}
