namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundItemUpdateDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefundItemUpdate", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefundItemUpdate", "Date");
        }
    }
}
