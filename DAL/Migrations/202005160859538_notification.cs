namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notification : DbMigration
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
                        NewsUserId = c.String(maxLength: 128),
                        NotificationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .ForeignKey("dbo.NotificationTypes", t => t.NotificationTypeId, cascadeDelete: true)
                .Index(t => t.NewsUserId)
                .Index(t => t.NotificationTypeId);
            
            CreateTable(
                "dbo.NotificationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "NotificationTypeId", "dbo.NotificationTypes");
            DropForeignKey("dbo.Notifications", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MessageRecipients", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MessageRecipients", "MessageId", "dbo.Messages");
            DropIndex("dbo.Notifications", new[] { "NotificationTypeId" });
            DropIndex("dbo.Notifications", new[] { "NewsUserId" });
            DropIndex("dbo.MessageRecipients", new[] { "NewsUserId" });
            DropIndex("dbo.MessageRecipients", new[] { "MessageId" });
            DropTable("dbo.NotificationTypes");
            DropTable("dbo.Notifications");
            DropTable("dbo.MessageRecipients");
        }
    }
}
