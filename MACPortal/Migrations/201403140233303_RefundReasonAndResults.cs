namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundReasonAndResults : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Refund", "Reason", c => c.String());
            AddColumn("dbo.Refund", "Results", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Refund", "Results");
            DropColumn("dbo.Refund", "Reason");
        }
    }
}
