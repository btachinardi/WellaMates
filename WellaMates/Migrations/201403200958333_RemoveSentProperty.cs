namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSentProperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Refund", "Sent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Refund", "Sent", c => c.Boolean(nullable: false));
        }
    }
}
