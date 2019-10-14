namespace dCC_GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class joiningTableCreation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interests", "Activity_Id", "dbo.Activities");
            DropForeignKey("dbo.Activities", "Vacation_Id", "dbo.Vacations");
            DropIndex("dbo.Activities", new[] { "Vacation_Id" });
            DropIndex("dbo.Interests", new[] { "Activity_Id" });
            DropColumn("dbo.Activities", "Vacation_Id");
            DropColumn("dbo.Interests", "Activity_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Interests", "Activity_Id", c => c.Int());
            AddColumn("dbo.Activities", "Vacation_Id", c => c.Int());
            CreateIndex("dbo.Interests", "Activity_Id");
            CreateIndex("dbo.Activities", "Vacation_Id");
            AddForeignKey("dbo.Activities", "Vacation_Id", "dbo.Vacations", "Id");
            AddForeignKey("dbo.Interests", "Activity_Id", "dbo.Activities", "Id");
        }
    }
}
