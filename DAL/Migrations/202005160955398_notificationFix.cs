namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificationFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "NewsUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "NewsUserId" });
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
            
            DropColumn("dbo.Notifications", "NewsUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "NewsUserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.NotificationNewsUsers", "NewsUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationNewsUsers", "Notification_Id", "dbo.Notifications");
            DropIndex("dbo.NotificationNewsUsers", new[] { "NewsUser_Id" });
            DropIndex("dbo.NotificationNewsUsers", new[] { "Notification_Id" });
            DropTable("dbo.NotificationNewsUsers");
            CreateIndex("dbo.Notifications", "NewsUserId");
            AddForeignKey("dbo.Notifications", "NewsUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
