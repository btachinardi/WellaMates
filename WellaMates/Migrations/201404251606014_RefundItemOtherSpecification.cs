namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefundItemOtherSpecification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefundItem", "OtherSpecification", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefundItem", "OtherSpecification");
        }
    }
}
