namespace dCC_GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testtttt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vacations", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Vacations", new[] { "Customer_Id" });
            RenameColumn(table: "dbo.Vacations", name: "Customer_Id", newName: "SavedCustomer");
            AlterColumn("dbo.Vacations", "SavedCustomer", c => c.Int(nullable: false));
            CreateIndex("dbo.Vacations", "SavedCustomer");
            AddForeignKey("dbo.Vacations", "SavedCustomer", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacations", "SavedCustomer", "dbo.Customers");
            DropIndex("dbo.Vacations", new[] { "SavedCustomer" });
            AlterColumn("dbo.Vacations", "SavedCustomer", c => c.Int());
            RenameColumn(table: "dbo.Vacations", name: "SavedCustomer", newName: "Customer_Id");
            CreateIndex("dbo.Vacations", "Customer_Id");
            AddForeignKey("dbo.Vacations", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
