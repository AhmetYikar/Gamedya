using DAL;
using Entities.Models;
using Entities.Models.TwitchYoutube;
using ServiceLayer.Repository.BlogRepository;
using ServiceLayer.Repository.ForumRepository;
using ServiceLayer.Repository.GameRepository;
using ServiceLayer.Repository.MessageRepository;
using ServiceLayer.Repository.NewsRepository;
using ServiceLayer.Repository.NotificationRepository;
using ServiceLayer.Repository.TwitchYoutubes;
using ServiceLayer.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly GameNewsDbContext _context;

        public UnitOfWork(GameNewsDbContext context)
        {
            _context = context;
            BlogComment = new BlogCommentRepository(_context);
            BlogImage = new BlogImageRepository(_context);
            BlogPost = new BlogPostRepository(_context);
            BlogCategory = new BlogCategoryRepository(_context);


            ForumCategory = new ForumCategoryRepository(_context);
            ForumImage = new ForumImageRepository(_context);
            ForumPost = new ForumPostRepository(_context);
            ForumReply = new ForumReplyRepository(_context);


            GameGenre = new GameGenreRepository(_context);
            Game = new GameRepository(_context);
            GameUpdate = new GameUpdateRepository(_context);


            News = new NewsRepository(_context);
            NewsCategory = new NewsCategoryRepository(_context);
            NewsComment = new NewsCommentRepositroy(_context);
            NewsImage = new NewsImageRepository(_context);
            NewsVideo = new NewsVideoRepository(_context);

            LikeTable = new LikeTableRepository(_context);
            NewsUser = new NewsUserRepository(_context);
            GamedyaMessage = new MessageRepository(_context);
            MessageRecipient = new MessageRecipientRepository(_context);
            Notification = new NotificationRepository(_context);
            TwitchYoutube = new TwitchYoutubeRepository(_context);


        }


        public IBlogCommentRepository BlogComment { get; private set; }
        public IBlogImageRepository BlogImage { get; private set; }
        public IBlogPostRepository BlogPost { get; private set; }
        public IBlogCategoryRepository BlogCategory { get; private set; }



        public IForumCategoryRepository ForumCategory { get; private set; }
        public IForumImageRepository ForumImage { get; private set; }
        public IForumPostRepository ForumPost { get; private set; }
        public IForumReplyRepository ForumReply { get; private set; }

        public IGameRepository Game { get; private set; }
        public IGameGenreRepository GameGenre { get; private set; }
        public IGameUpdateRepository GameUpdate { get; private set; }

        public INewsCategoryRepository NewsCategory { get; private set; }
        public INewsCommentRepositroy NewsComment { get; private set; }
        public INewsImageRepository NewsImage { get; private set; }
        public INewsRepository News { get; private set; }
        public INewsVideoRepository NewsVideo { get; private set; }

        public ILikeTableRepository LikeTable { get; private set; }
        public INewsUserRepository NewsUser { get; private set; }
        public IMessageRepository GamedyaMessage { get; private set; }
        public IMessageRecipientRepository MessageRecipient { get; private set; }

        public INotificationRepository Notification { get; private set; }

        public ITwitchYoutubeRepository TwitchYoutube { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
