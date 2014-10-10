namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BiggerRefundItemActivityString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefundItem", "Activity", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefundItem", "Activity", c => c.String(nullable: false, maxLength: 40));
        }
    }
}
