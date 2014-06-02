namespace WellaMates.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        RecoverPasswordToken = c.String(),
                        RecoverPasswordExpiration = c.DateTime(),
                        CurrentAcceptedAgreement = c.String(),
                        AcceptSMS = c.String(),
                        AcceptEmail = c.String(),
                        PersonalInfo_Name = c.String(),
                        PersonalInfo_CPF = c.String(),
                        PersonalInfo_Birthday = c.DateTime(),
                        PersonalInfo_Gender = c.Int(),
                        ContactInfo_Email = c.String(),
                        ContactInfo_HomePhone = c.String(),
                        ContactInfo_CellPhone = c.String(),
                        AddressInfo_CEP = c.String(),
                        AddressInfo_Street = c.String(),
                        AddressInfo_Number = c.String(),
                        AddressInfo_Complement = c.String(),
                        AddressInfo_Neighborhood = c.String(),
                        AddressInfo_City = c.String(),
                        AddressInfo_State = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.RefundProfile",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.UserProfile", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Freelancer",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        WorkDays = c.Int(nullable: false),
                        Remuneration = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MealAssistance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TelephoneAssistance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManagerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Manager", t => t.ManagerID, cascadeDelete: true)
                .ForeignKey("dbo.RefundProfile", t => t.UserID)
                .Index(t => t.ManagerID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.RefundProfile", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.RefundAdministrator",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.RefundProfile", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.RefundItemUpdate",
                c => new
                    {
                        RefundItemUpdateID = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        ImageUrl = c.String(),
                        Status = c.Int(nullable: false),
                        RefundProfileID = c.Int(nullable: false),
                        RefundProfile_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.RefundItemUpdateID)
                .ForeignKey("dbo.RefundProfile", t => t.RefundProfile_UserID)
                .Index(t => t.RefundProfile_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefundProfile", "UserID", "dbo.UserProfile");
            DropForeignKey("dbo.RefundItemUpdate", "RefundProfile_UserID", "dbo.RefundProfile");
            DropForeignKey("dbo.RefundAdministrator", "UserID", "dbo.RefundProfile");
            DropForeignKey("dbo.Freelancer", "UserID", "dbo.RefundProfile");
            DropForeignKey("dbo.Manager", "UserID", "dbo.RefundProfile");
            DropForeignKey("dbo.Freelancer", "ManagerID", "dbo.Manager");
            DropIndex("dbo.RefundProfile", new[] { "UserID" });
            DropIndex("dbo.RefundItemUpdate", new[] { "RefundProfile_UserID" });
            DropIndex("dbo.RefundAdministrator", new[] { "UserID" });
            DropIndex("dbo.Freelancer", new[] { "UserID" });
            DropIndex("dbo.Manager", new[] { "UserID" });
            DropIndex("dbo.Freelancer", new[] { "ManagerID" });
            DropTable("dbo.RefundItemUpdate");
            DropTable("dbo.RefundAdministrator");
            DropTable("dbo.Manager");
            DropTable("dbo.Freelancer");
            DropTable("dbo.RefundProfile");
            DropTable("dbo.UserProfile");
        }
    }
}
