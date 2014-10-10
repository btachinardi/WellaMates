namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundItemUpdateIds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefundItemUpdate", "RefundItemID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefundItemUpdate", "RefundItemID");
        }
    }
}
