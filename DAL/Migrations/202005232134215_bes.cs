namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameUpdates", "ArticleType", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameUpdates", "ArticleType");
        }
    }
}
