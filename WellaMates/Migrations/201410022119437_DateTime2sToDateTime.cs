namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTime2sToDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Event", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Visit", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Visit", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Event", "EndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Event", "StartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
