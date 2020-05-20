namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messageSenderName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GamedyaMessages", "SenderName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GamedyaMessages", "SenderName");
        }
    }
}
