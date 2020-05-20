namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yeniden : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 255),
                        ParentId = c.Int(),
                        BlogCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategory_Id)
                .Index(t => t.BlogCategory_Id);
            
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Summary = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                        TinyImagePath = c.String(),
                        BlogCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.NewsUserId)
                .Index(t => t.BlogCategoryId);
            
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        BlogPostId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IsOk = c.Boolean(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.BlogPostId)
                .Index(t => t.NewsUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Image = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ForumTitle = c.String(nullable: false, maxLength: 255),
                        Summary = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsOk = c.Boolean(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                        TinyImagePath = c.String(),
                        ForumCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumCategories", t => t.ForumCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.NewsUserId)
                .Index(t => t.ForumCategoryId);
            
            CreateTable(
                "dbo.ForumCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 150),
                        Description = c.String(maxLength: 255),
                        ParentId = c.Int(),
                        ForumCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumCategories", t => t.ForumCategory_Id)
                .Index(t => t.ForumCategory_Id);
            
            CreateTable(
                "dbo.ForumImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(maxLength: 255),
                        ForumPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumPosts", t => t.ForumPostId, cascadeDelete: true)
                .Index(t => t.ForumPostId);
            
            CreateTable(
                "dbo.ForumReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ForumPostId = c.Int(nullable: false),
                        Content = c.String(),
                        Date = c.DateTime(nullable: false),
                        IsOk = c.Boolean(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumPosts", t => t.ForumPostId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.ForumPostId)
                .Index(t => t.NewsUserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MessageRecipients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsRead = c.Boolean(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.NewsUserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                        MessageRecipientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MessageRecipients", t => t.MessageRecipientId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.NewsUserId)
                .Index(t => t.MessageRecipientId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Publisher = c.String(),
                        NewsUserId = c.String(maxLength: 128),
                        ReleaseDate = c.DateTime(nullable: false),
                        GameGenreId = c.Int(nullable: false),
                        GamePlatform = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameGenres", t => t.GameGenreId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.NewsUserId)
                .Index(t => t.GameGenreId);
            
            CreateTable(
                "dbo.GameGenres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenreName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GameUpdates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        GameId = c.Int(nullable: false),
                        ArticleType = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.NewsComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsOk = c.Boolean(nullable: false),
                        NewsId = c.Int(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        GuestName = c.String(),
                        LikeCount = c.Int(nullable: false),
                        UnLikeCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.NewsId)
                .Index(t => t.NewsUserId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        Summary = c.String(),
                        Content = c.String(),
                        NewsUserId = c.String(maxLength: 128),
                        NewsCategoryId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        EditDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        TinyImagePath = c.String(),
                        NewsPlatform = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsCategories", t => t.NewsCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.NewsUserId)
                .Index(t => t.NewsCategoryId);
            
            CreateTable(
                "dbo.NewsCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 150),
                        Description = c.String(maxLength: 255),
                        ParentId = c.Int(),
                        NewsCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NewsCategories", t => t.NewsCategory_Id)
                .Index(t => t.NewsCategory_Id);
            
            CreateTable(
                "dbo.NewsImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(maxLength: 255),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.NewsVideos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VideoPath = c.String(maxLength: 255),
                        NewsId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewsId, cascadeDelete: true)
                .Index(t => t.NewsId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Module = c.Int(nullable: false),
                        NModuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.BlogImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(maxLength: 255),
                        BlogPostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogPosts", t => t.BlogPostId, cascadeDelete: true)
                .Index(t => t.BlogPostId);
            
            CreateTable(
                "dbo.LikeTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Module = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                        NewsUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUserId)
                .Index(t => t.NewsUserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.NotificationNewsUsers",
                c => new
                    {
                        Notification_Id = c.Int(nullable: false),
                        NewsUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Notification_Id, t.NewsUser_Id })
                .ForeignKey("dbo.Notifications", t => t.Notification_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.NewsUser_Id, cascadeDelete: true)
                .Index(t => t.Notification_Id)
                .Index(t => t.NewsUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LikeTables", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogImages", "BlogPostId", "dbo.BlogPosts");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationNewsUsers", "NewsUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationNewsUsers", "Notification_Id", "dbo.Notifications");
            DropForeignKey("dbo.NewsComments", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NewsVideos", "NewsId", "dbo.News");
            DropForeignKey("dbo.News", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NewsImages", "NewsId", "dbo.News");
            DropForeignKey("dbo.NewsComments", "NewsId", "dbo.News");
            DropForeignKey("dbo.NewsCategories", "NewsCategory_Id", "dbo.NewsCategories");
            DropForeignKey("dbo.News", "NewsCategoryId", "dbo.NewsCategories");
            DropForeignKey("dbo.Games", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GameUpdates", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "GameGenreId", "dbo.GameGenres");
            DropForeignKey("dbo.MessageRecipients", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "MessageRecipientId", "dbo.MessageRecipients");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumPosts", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumReplies", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumReplies", "ForumPostId", "dbo.ForumPosts");
            DropForeignKey("dbo.ForumImages", "ForumPostId", "dbo.ForumPosts");
            DropForeignKey("dbo.ForumPosts", "ForumCategoryId", "dbo.ForumCategories");
            DropForeignKey("dbo.ForumCategories", "ForumCategory_Id", "dbo.ForumCategories");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogPosts", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogComments", "NewsUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BlogComments", "BlogPostId", "dbo.BlogPosts");
            DropForeignKey("dbo.BlogPosts", "BlogCategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogCategories", "BlogCategory_Id", "dbo.BlogCategories");
            DropIndex("dbo.NotificationNewsUsers", new[] { "NewsUser_Id" });
            DropIndex("dbo.NotificationNewsUsers", new[] { "Notification_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.LikeTables", new[] { "NewsUserId" });
            DropIndex("dbo.BlogImages", new[] { "BlogPostId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.NewsVideos", new[] { "NewsId" });
            DropIndex("dbo.NewsImages", new[] { "NewsId" });
            DropIndex("dbo.NewsCategories", new[] { "NewsCategory_Id" });
            DropIndex("dbo.News", new[] { "NewsCategoryId" });
            DropIndex("dbo.News", new[] { "NewsUserId" });
            DropIndex("dbo.NewsComments", new[] { "NewsUserId" });
            DropIndex("dbo.NewsComments", new[] { "NewsId" });
            DropIndex("dbo.GameUpdates", new[] { "GameId" });
            DropIndex("dbo.Games", new[] { "GameGenreId" });
            DropIndex("dbo.Games", new[] { "NewsUserId" });
            DropIndex("dbo.Messages", new[] { "MessageRecipientId" });
            DropIndex("dbo.Messages", new[] { "NewsUserId" });
            DropIndex("dbo.MessageRecipients", new[] { "NewsUserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.ForumReplies", new[] { "NewsUserId" });
            DropIndex("dbo.ForumReplies", new[] { "ForumPostId" });
            DropIndex("dbo.ForumImages", new[] { "ForumPostId" });
            DropIndex("dbo.ForumCategories", new[] { "ForumCategory_Id" });
            DropIndex("dbo.ForumPosts", new[] { "ForumCategoryId" });
            DropIndex("dbo.ForumPosts", new[] { "NewsUserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BlogComments", new[] { "NewsUserId" });
            DropIndex("dbo.BlogComments", new[] { "BlogPostId" });
            DropIndex("dbo.BlogPosts", new[] { "BlogCategoryId" });
            DropIndex("dbo.BlogPosts", new[] { "NewsUserId" });
            DropIndex("dbo.BlogCategories", new[] { "BlogCategory_Id" });
            DropTable("dbo.NotificationNewsUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.LikeTables");
            DropTable("dbo.BlogImages");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Notifications");
            DropTable("dbo.NewsVideos");
            DropTable("dbo.NewsImages");
            DropTable("dbo.NewsCategories");
            DropTable("dbo.News");
            DropTable("dbo.NewsComments");
            DropTable("dbo.GameUpdates");
            DropTable("dbo.GameGenres");
            DropTable("dbo.Games");
            DropTable("dbo.Messages");
            DropTable("dbo.MessageRecipients");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.ForumReplies");
            DropTable("dbo.ForumImages");
            DropTable("dbo.ForumCategories");
            DropTable("dbo.ForumPosts");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BlogComments");
            DropTable("dbo.BlogPosts");
            DropTable("dbo.BlogCategories");
        }
    }
}
