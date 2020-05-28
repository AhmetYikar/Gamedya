namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commenterName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogComments", "CommenterName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogComments", "CommenterName");
        }
    }
}
