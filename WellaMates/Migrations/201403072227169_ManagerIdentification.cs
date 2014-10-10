namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManagerIdentification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Manager", "Identification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Manager", "Identification");
        }
    }
}
