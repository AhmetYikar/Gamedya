namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "NewsPart", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "NewsPart");
        }
    }
}
