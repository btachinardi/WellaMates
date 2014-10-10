using WellaMates.DAL;

namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraDataAnnotations : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.RefundItem SET Activity='NOT NULL' WHERE Activity IS NULL");
            AlterColumn("dbo.RefundItem", "Activity", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Refund", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Refund", "Reason", c => c.String(nullable: false));
            AlterColumn("dbo.Refund", "Results", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Refund", "Results", c => c.String());
            AlterColumn("dbo.Refund", "Reason", c => c.String());
            AlterColumn("dbo.Refund", "Name", c => c.String());
            AlterColumn("dbo.RefundItem", "Activity", c => c.String());
        }
    }
}
