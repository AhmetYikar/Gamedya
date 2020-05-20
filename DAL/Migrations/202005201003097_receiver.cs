namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class receiver : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GamedyaMessages", "ReceiverName", c => c.String());
            DropColumn("dbo.MessageRecipients", "ReceiverName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageRecipients", "ReceiverName", c => c.String());
            DropColumn("dbo.GamedyaMessages", "ReceiverName");
        }
    }
}
