namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceivedInvoiceUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefundItemUpdate", "ReceivedInvoice", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefundItemUpdate", "ReceivedInvoice");
        }
    }
}
