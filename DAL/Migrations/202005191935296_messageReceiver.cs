namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messageReceiver : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageRecipients", "ReceiverName", c => c.String());
            AddColumn("dbo.Messages", "IsRead", c => c.Boolean(nullable: false));
            DropColumn("dbo.MessageRecipients", "IsRead");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageRecipients", "IsRead", c => c.Boolean(nullable: false));
            DropColumn("dbo.Messages", "IsRead");
            DropColumn("dbo.MessageRecipients", "ReceiverName");
        }
    }
}
