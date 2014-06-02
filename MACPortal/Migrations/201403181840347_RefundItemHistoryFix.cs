namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundItemHistoryFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RefundItemUpdate", "RefundItemUpdateID", "dbo.RefundItem");
            DropIndex("dbo.RefundItemUpdate", new[] { "RefundItemUpdateID" });
            AlterColumn("dbo.RefundItemUpdate", "RefundItemUpdateID", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.RefundItemUpdate", "RefundItemID");
            AddForeignKey("dbo.RefundItemUpdate", "RefundItemID", "dbo.RefundItem", "RefundItemID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefundItemUpdate", "RefundItemID", "dbo.RefundItem");
            DropIndex("dbo.RefundItemUpdate", new[] { "RefundItemID" });
            AlterColumn("dbo.RefundItemUpdate", "RefundItemUpdateID", c => c.Int(nullable: false));
            CreateIndex("dbo.RefundItemUpdate", "RefundItemUpdateID");
            AddForeignKey("dbo.RefundItemUpdate", "RefundItemUpdateID", "dbo.RefundItem", "RefundItemID");
        }
    }
}
