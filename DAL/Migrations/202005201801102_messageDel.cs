namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messageDel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GamedyaMessages", "ReceiverDel", c => c.Boolean(nullable: false));
            AddColumn("dbo.GamedyaMessages", "SenderDel", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GamedyaMessages", "SenderDel");
            DropColumn("dbo.GamedyaMessages", "ReceiverDel");
        }
    }
}
