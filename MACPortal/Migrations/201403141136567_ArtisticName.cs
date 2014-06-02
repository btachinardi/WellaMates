namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtisticName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "PersonalInfo_ArtisticName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfile", "PersonalInfo_ArtisticName");
        }
    }
}
