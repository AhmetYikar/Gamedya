namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotificationUsers", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationUsers", "NotificationId", "dbo.Notifications");
            DropIndex("dbo.NotificationUsers", new[] { "NotificationId" });
            DropIndex("dbo.NotificationUsers", new[] { "NewsUserId" });
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
            
            DropTable("dbo.NotificationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NotificationUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationId = c.Int(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.NotificationNewsUsers", "NewsUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationNewsUsers", "Notification_Id", "dbo.Notifications");
            DropIndex("dbo.NotificationNewsUsers", new[] { "NewsUser_Id" });
            DropIndex("dbo.NotificationNewsUsers", new[] { "Notification_Id" });
            DropTable("dbo.NotificationNewsUsers");
            CreateIndex("dbo.NotificationUsers", "NewsUserId");
            CreateIndex("dbo.NotificationUsers", "NotificationId");
            AddForeignKey("dbo.NotificationUsers", "NotificationId", "dbo.Notifications", "Id", cascadeDelete: true);
            AddForeignKey("dbo.NotificationUsers", "NewsUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
