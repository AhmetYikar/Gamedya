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
    public interface IUnitOfWork : IDisposable
    {
        IBlogCommentRepository BlogComment { get; }
        IBlogImageRepository BlogImage { get; }
        IBlogPostRepository BlogPost { get; }
        IBlogCategoryRepository BlogCategory { get; }

        IForumCategoryRepository ForumCategory { get; }
        IForumImageRepository ForumImage { get; }
        IForumPostRepository ForumPost { get; }
        IForumReplyRepository ForumReply { get; }

        IGameRepository Game { get; }
        IGameGenreRepository GameGenre { get; }
        IGameUpdateRepository GameUpdate { get; }

        INewsCommentRepositroy NewsComment { get; }
        INewsImageRepository NewsImage { get; }
        INewsRepository News { get; }
        INewsVideoRepository NewsVideo { get; }

        ILikeTableRepository LikeTable { get; }

        INewsUserRepository NewsUser { get; }
        IMessageRepository GamedyaMessage { get; }
        IMessageRecipientRepository MessageRecipient { get; }

        INotificationRepository Notification { get; }

        ITwitchYoutubeRepository TwitchYoutube { get; }

        int Complete();
    }
}
