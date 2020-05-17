namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificationUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotificationNewsUsers", "Notification_Id", "dbo.Notifications");
            DropForeignKey("dbo.NotificationNewsUsers", "NewsUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.NotificationNewsUsers", new[] { "Notification_Id" });
            DropIndex("dbo.NotificationNewsUsers", new[] { "NewsUser_Id" });
            CreateTable(
                "dbo.NotificationUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationId = c.Int(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .Index(t => t.NotificationId)
                .Index(t => t.NewsUserId);
            
            DropTable("dbo.NotificationNewsUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NotificationNewsUsers",
                c => new
                    {
                        Notification_Id = c.Int(nullable: false),
                        NewsUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Notification_Id, t.NewsUser_Id });
            
            DropForeignKey("dbo.NotificationUsers", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.NotificationUsers", "NewsUserId", "dbo.AspNetUsers");
            DropIndex("dbo.NotificationUsers", new[] { "NewsUserId" });
            DropIndex("dbo.NotificationUsers", new[] { "NotificationId" });
            DropTable("dbo.NotificationUsers");
            CreateIndex("dbo.NotificationNewsUsers", "NewsUser_Id");
            CreateIndex("dbo.NotificationNewsUsers", "Notification_Id");
            AddForeignKey("dbo.NotificationNewsUsers", "NewsUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.NotificationNewsUsers", "Notification_Id", "dbo.Notifications", "Id", cascadeDelete: true);
        }
    }
}
