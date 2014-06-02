namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.File", "RefundItemUpdate_RefundItemUpdateID", c => c.Int());
            AddColumn("dbo.Refund", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Refund", "AcceptedValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.File", "RefundItemUpdate_RefundItemUpdateID");
            AddForeignKey("dbo.File", "RefundItemUpdate_RefundItemUpdateID", "dbo.RefundItemUpdate", "RefundItemUpdateID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.File", "RefundItemUpdate_RefundItemUpdateID", "dbo.RefundItemUpdate");
            DropIndex("dbo.File", new[] { "RefundItemUpdate_RefundItemUpdateID" });
            DropColumn("dbo.Refund", "AcceptedValue");
            DropColumn("dbo.Refund", "Value");
            DropColumn("dbo.File", "RefundItemUpdate_RefundItemUpdateID");
        }
    }
}
