namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Visualizators : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefundVisualizator",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.RefundProfile", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefundVisualizator", "UserID", "dbo.RefundProfile");
            DropIndex("dbo.RefundVisualizator", new[] { "UserID" });
            DropTable("dbo.RefundVisualizator");
        }
    }
}
