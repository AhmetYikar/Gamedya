namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificationModule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Module", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "NModuleId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "NModuleId");
            DropColumn("dbo.Notifications", "Module");
        }
    }
}
