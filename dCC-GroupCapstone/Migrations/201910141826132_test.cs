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
                        Vacation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vacations", t => t.Vacation_Id)
                .Index(t => t.Vacation_Id);
            
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Activity_Id = c.Int(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.Activity_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Activity_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Vacations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        IsPrivate = c.Boolean(nullable: false),
                        LocationQueried = c.String(),
                        Cost = c.Double(nullable: false),
                        SavedHotel = c.Int(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.SavedHotel, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.UserId)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Vacations", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Vacations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Activities", "Vacation_Id", "dbo.Vacations");
            DropForeignKey("dbo.Vacations", "SavedHotel", "dbo.Hotels");
            DropForeignKey("dbo.Interests", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Interests", "Activity_Id", "dbo.Activities");
            DropIndex("dbo.Vacations", new[] { "Customer_Id" });
            DropIndex("dbo.Vacations", new[] { "SavedHotel" });
            DropIndex("dbo.Vacations", new[] { "UserId" });
            DropIndex("dbo.Customers", new[] { "UserId" });
            DropIndex("dbo.Interests", new[] { "Customer_Id" });
            DropIndex("dbo.Interests", new[] { "Activity_Id" });
            DropIndex("dbo.Activities", new[] { "Vacation_Id" });
            DropTable("dbo.Hotels");
            DropTable("dbo.Vacations");
            DropTable("dbo.Customers");
            DropTable("dbo.Interests");
            DropTable("dbo.Activities");
        }
    }
}
