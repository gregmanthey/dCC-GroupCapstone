namespace dCC_GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PlaceId = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vacations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VacationName = c.String(),
                        IsPrivate = c.Boolean(nullable: false),
                        LocationQueried = c.String(),
                        CustomerCreated = c.Int(nullable: false),
                        Cost = c.Double(nullable: false),
                        SavedHotel = c.Int(nullable: false),
                        AverageRating = c.Double(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.SavedHotel, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.SavedHotel)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PlaceId = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        InterestsSerialized = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        CustomerId = c.Int(nullable: false),
                        VacationId = c.Int(nullable: false),
                        RatingValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerId, t.VacationId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Vacations", t => t.VacationId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.VacationId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.VacationActivities",
                c => new
                    {
                        Vacation_Id = c.Int(nullable: false),
                        Activity_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Vacation_Id, t.Activity_Id })
                .ForeignKey("dbo.Vacations", t => t.Vacation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Activities", t => t.Activity_Id, cascadeDelete: true)
                .Index(t => t.Vacation_Id)
                .Index(t => t.Activity_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ratings", "VacationId", "dbo.Vacations");
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vacations", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Vacations", "SavedHotel", "dbo.Hotels");
            DropForeignKey("dbo.VacationActivities", "Activity_Id", "dbo.Activities");
            DropForeignKey("dbo.VacationActivities", "Vacation_Id", "dbo.Vacations");
            DropIndex("dbo.VacationActivities", new[] { "Activity_Id" });
            DropIndex("dbo.VacationActivities", new[] { "Vacation_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ratings", new[] { "VacationId" });
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropIndex("dbo.Vacations", new[] { "Customer_Id" });
            DropIndex("dbo.Vacations", new[] { "SavedHotel" });
            DropTable("dbo.VacationActivities");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Ratings");
            DropTable("dbo.Interests");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Customers");
            DropTable("dbo.Hotels");
            DropTable("dbo.Vacations");
            DropTable("dbo.Activities");
        }
    }
}
