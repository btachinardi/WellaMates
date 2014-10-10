namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersistentRefundStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Refund", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Refund", "Status");
        }
    }
}
