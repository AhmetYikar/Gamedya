namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ilk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TwitchYoutubes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        VideoPath = c.String(),
                        CoverImage = c.String(),
                        Date = c.DateTime(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        VideoPlatform = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TwitchYoutubes");
        }
    }
}
