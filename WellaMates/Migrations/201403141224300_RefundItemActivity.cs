namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundItemActivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefundItem", "Activity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefundItem", "Activity");
        }
    }
}
