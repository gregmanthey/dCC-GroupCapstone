namespace dCC_GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vacations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Interests", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Vacations", new[] { "UserId" });
            DropIndex("dbo.Interests", new[] { "Customer_Id" });
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
                "dbo.InterestCustomers",
                c => new
                    {
                        Interest_Id = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Interest_Id, t.Customer_Id })
                .ForeignKey("dbo.Interests", t => t.Interest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Interest_Id)
                .Index(t => t.Customer_Id);
            
            DropColumn("dbo.Vacations", "UserId");
            DropColumn("dbo.Interests", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Interests", "Customer_Id", c => c.Int());
            AddColumn("dbo.Vacations", "UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Ratings", "VacationId", "dbo.Vacations");
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.InterestCustomers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.InterestCustomers", "Interest_Id", "dbo.Interests");
            DropIndex("dbo.InterestCustomers", new[] { "Customer_Id" });
            DropIndex("dbo.InterestCustomers", new[] { "Interest_Id" });
            DropIndex("dbo.Ratings", new[] { "VacationId" });
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropTable("dbo.InterestCustomers");
            DropTable("dbo.Ratings");
            CreateIndex("dbo.Interests", "Customer_Id");
            CreateIndex("dbo.Vacations", "UserId");
            AddForeignKey("dbo.Interests", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Vacations", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
