namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.File", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.File", "FilePath");
        }
    }
}
