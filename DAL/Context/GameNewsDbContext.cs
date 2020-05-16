using Entites.Models.BlogModels;
using Entites.Models.ForumModels;
using Entites.Models.GameModels;
using Entites.Models.NewsModels;
using Entites.Models.MessageModels;
using Entites.Models.Status;
using Entites.Models.UserModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace DAL
{


    public class GameNewsDbContext : IdentityDbContext<NewsUser>
    {
        public GameNewsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static GameNewsDbContext Create()
        {
            return new GameNewsDbContext();
        }

        public System.Data.Entity.DbSet<News> News { get; set; }
        public System.Data.Entity.DbSet<NewsComment> NewsComments { get; set; }
        public System.Data.Entity.DbSet<NewsImage> NewsImages { get; set; }
        public System.Data.Entity.DbSet<NewsVideo> NewsVideos { get; set; }
        public System.Data.Entity.DbSet<NewsCategory> NewsCategories { get; set; }


        public System.Data.Entity.DbSet<BlogPost> BlogPosts { get; set; }
        public System.Data.Entity.DbSet<BlogImage> BlogImages { get; set; }
        public System.Data.Entity.DbSet<BlogComment> BlogComments { get; set; }

        public System.Data.Entity.DbSet<ForumCategory> ForumCategories { get; set; }
        public System.Data.Entity.DbSet<ForumImage> ForumImages { get; set; }
        public System.Data.Entity.DbSet<ForumPost> ForumPosts { get; set; }
        public System.Data.Entity.DbSet<ForumReply> ForumReplies { get; set; }

        public System.Data.Entity.DbSet<Game> Games { get; set; }


        public System.Data.Entity.DbSet<GameGenre> GameGenres { get; set; }
        public System.Data.Entity.DbSet<GameUpdate> GameUpdates { get; set; }

        public System.Data.Entity.DbSet<LikeTable> LikeTables { get; set; }

        public System.Data.Entity.DbSet<BlogCategory> BlogCategories { get; set; }

        public System.Data.Entity.DbSet<Message> Messages { get; set; }
        public System.Data.Entity.DbSet<MessageRecipient> MessageRecipients { get; set; }
        public System.Data.Entity.DbSet<Notification> Notifications { get; set; }


    }
}
