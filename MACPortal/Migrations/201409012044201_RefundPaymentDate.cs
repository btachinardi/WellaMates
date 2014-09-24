namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundPaymentDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Refund", "PaymentDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Refund", "PaymentDate");
        }
    }
}
