namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ForumPosts", "Content", c => c.String());
        }
        
        public override void Down()
        {
           
            AlterColumn("dbo.ForumPosts", "Content", c => c.String(nullable: false));
        }
    }
}
