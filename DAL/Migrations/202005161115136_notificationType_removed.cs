namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificationType_removed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notifications", "NotificationTypeId", "dbo.NotificationTypes");
            DropIndex("dbo.Notifications", new[] { "NotificationTypeId" });
            DropColumn("dbo.Notifications", "NotificationTypeId");
            DropTable("dbo.NotificationTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NotificationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Notifications", "NotificationTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Notifications", "NotificationTypeId");
            AddForeignKey("dbo.Notifications", "NotificationTypeId", "dbo.NotificationTypes", "Id", cascadeDelete: true);
        }
    }
}
