namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundDateTime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Refund", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Refund", "Date", c => c.DateTime(nullable: false));
        }
    }
}
