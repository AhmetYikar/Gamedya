namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ForumImages", "ForumPostId", "dbo.ForumPosts");
            DropIndex("dbo.ForumImages", new[] { "ForumPostId" });
            AlterColumn("dbo.ForumPosts", "Content", c => c.String());
            DropColumn("dbo.ForumPosts", "Summary");
            DropColumn("dbo.ForumPosts", "EditDate");
            DropColumn("dbo.ForumPosts", "TinyImagePath");
            DropTable("dbo.ForumImages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ForumImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(maxLength: 255),
                        ForumPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ForumPosts", "TinyImagePath", c => c.String());
            AddColumn("dbo.ForumPosts", "EditDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ForumPosts", "Summary", c => c.String(nullable: false));
            AlterColumn("dbo.ForumPosts", "Content", c => c.String(nullable: false));
            CreateIndex("dbo.ForumImages", "ForumPostId");
            AddForeignKey("dbo.ForumImages", "ForumPostId", "dbo.ForumPosts", "Id", cascadeDelete: true);
        }
    }
}
