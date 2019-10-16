namespace dCC_GroupCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vacations", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vacations", "Name");
        }
    }
}
