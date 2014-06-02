namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundSentBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Refund", "Sent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Refund", "Sent");
        }
    }
}
