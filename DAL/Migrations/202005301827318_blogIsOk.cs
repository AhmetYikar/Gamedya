namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blogIsOk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "IsOk", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogPosts", "IsOk");
        }
    }
}
