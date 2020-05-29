namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iki : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.News", "NewsPlatform", c => c.Byte());
            AlterColumn("dbo.News", "NewsPart", c => c.Byte());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "NewsPart", c => c.Byte(nullable: false));
            AlterColumn("dbo.News", "NewsPlatform", c => c.Byte(nullable: false));
        }
    }
}
