namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dort : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameUpdates", "GameId", c => c.Int(nullable: false));
            DropColumn("dbo.GameUpdates", "Game_Id");
            AddColumn("dbo.GameUpdates", "ArticleType", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameUpdates", "GameId");
            AddColumn("dbo.GameUpdates", "Game_Id", c => c.Int(nullable: false));
            DropColumn("dbo.GameUpdates", "ArticleType");
        }
    }
}
