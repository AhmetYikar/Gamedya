namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likeCount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogComments", "LikeCount", c => c.Int(nullable: false));
            AddColumn("dbo.BlogComments", "UnLikeCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogComments", "UnLikeCount");
            DropColumn("dbo.BlogComments", "LikeCount");
        }
    }
}
