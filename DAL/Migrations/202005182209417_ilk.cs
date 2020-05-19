namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ilk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageRecipients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsRead = c.Boolean(nullable: false),
                        MessageId = c.Int(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.MessageId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.MessageId)
                .Index(t => t.NewsUserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Module = c.Int(nullable: false),
                        NModuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotificationNewsUsers",
                c => new
                    {
                        Notification_Id = c.Int(nullable: false),
                        NewsUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Notification_Id, t.NewsUser_Id })
                .ForeignKey("dbo.Notifications", t => t.Notification_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUser_Id, cascadeDelete: true)
                .Index(t => t.Notification_Id)
                .Index(t => t.NewsUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationNewsUsers", "NewsUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationNewsUsers", "Notification_Id", "dbo.Notifications");
            DropForeignKey("dbo.MessageRecipients", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MessageRecipients", "MessageId", "dbo.Messages");
            DropIndex("dbo.NotificationNewsUsers", new[] { "NewsUser_Id" });
            DropIndex("dbo.NotificationNewsUsers", new[] { "Notification_Id" });
            DropIndex("dbo.MessageRecipients", new[] { "NewsUserId" });
            DropIndex("dbo.MessageRecipients", new[] { "MessageId" });
            DropTable("dbo.NotificationNewsUsers");
            DropTable("dbo.Notifications");
            DropTable("dbo.MessageRecipients");
        }
    }
}
