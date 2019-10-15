namespace dCC_GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interests", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Interests", new[] { "Customer_Id" });
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
            
            DropColumn("dbo.Interests", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Interests", "Customer_Id", c => c.Int());
            DropForeignKey("dbo.InterestCustomers", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.InterestCustomers", "Interest_Id", "dbo.Interests");
            DropIndex("dbo.InterestCustomers", new[] { "Customer_Id" });
            DropIndex("dbo.InterestCustomers", new[] { "Interest_Id" });
            DropTable("dbo.InterestCustomers");
            CreateIndex("dbo.Interests", "Customer_Id");
            AddForeignKey("dbo.Interests", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
