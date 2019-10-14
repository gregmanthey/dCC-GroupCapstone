namespace dCC_GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attempt : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.VacationActivities", "Activity_Id", "dbo.Activities");
            DropForeignKey("dbo.VacationActivities", "Vacation_Id", "dbo.Vacations");
            DropIndex("dbo.VacationActivities", new[] { "Activity_Id" });
            DropIndex("dbo.VacationActivities", new[] { "Vacation_Id" });
            DropTable("dbo.VacationActivities");
        }
    }
}
