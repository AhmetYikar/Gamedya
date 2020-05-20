namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iki : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MessageRecipients", "MessageId", "dbo.Messages");
            DropForeignKey("dbo.Messages", "NewsUserId", "dbo.AspNetUsers");
            DropIndex("dbo.MessageRecipients", new[] { "MessageId" });
            DropIndex("dbo.Messages", new[] { "NewsUserId" });
            CreateTable(
                "dbo.GamedyaMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        ReceiverName = c.String(),
                        SenderName = c.String(),
                        ReceiverDel = c.Boolean(nullable: false),
                        SenderDel = c.Boolean(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                        MessageRecipientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MessageRecipients", t => t.MessageRecipientId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.NewsUserId)
                .Index(t => t.MessageRecipientId);
            
            DropColumn("dbo.MessageRecipients", "IsRead");
            DropColumn("dbo.MessageRecipients", "MessageId");
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MessageRecipients", "MessageId", c => c.Int(nullable: false));
            AddColumn("dbo.MessageRecipients", "IsRead", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.GamedyaMessages", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GamedyaMessages", "MessageRecipientId", "dbo.MessageRecipients");
            DropIndex("dbo.GamedyaMessages", new[] { "MessageRecipientId" });
            DropIndex("dbo.GamedyaMessages", new[] { "NewsUserId" });
            DropTable("dbo.GamedyaMessages");
            CreateIndex("dbo.Messages", "NewsUserId");
            CreateIndex("dbo.MessageRecipients", "MessageId");
            AddForeignKey("dbo.Messages", "NewsUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.MessageRecipients", "MessageId", "dbo.Messages", "Id", cascadeDelete: true);
        }
    }
}
