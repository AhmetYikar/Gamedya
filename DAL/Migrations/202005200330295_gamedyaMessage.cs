namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gamedyaMessage : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Messages", newName: "GamedyaMessages");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.GamedyaMessages", newName: "Messages");
        }
    }
}
