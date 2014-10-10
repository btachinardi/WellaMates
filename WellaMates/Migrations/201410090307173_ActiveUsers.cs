namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActiveUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "Active");
        }
    }
}
