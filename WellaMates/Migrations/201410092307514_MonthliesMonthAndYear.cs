using WellaMates.DAL;
using WellaMates.Models;

namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MonthliesMonthAndYear : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Monthly", "Month", c => c.String(nullable: true));
            RenameColumn("dbo.Monthly", "Month", "MonthText");
            AddColumn("dbo.Monthly", "Year", c => c.Int(nullable: false));
            AddColumn("dbo.Monthly", "Month", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Monthly", "Month");
            DropColumn("dbo.Monthly", "Year");
            RenameColumn("dbo.Monthly", "MonthText", "Month");
            AlterColumn("dbo.Monthly", "Month", c => c.String(nullable: false));
        }
    }
}
