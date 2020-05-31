using DAL;
using Entites.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace ServiceLayer.Repository.UserRepository
{
    public class NewsUserRepository : GenericRepository<NewsUser>, INewsUserRepository
    {
        public NewsUserRepository(GameNewsDbContext context)
        : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

        public IEnumerable<NewsUser> UserWithGames()
        {
            return _context.Set<NewsUser>().Include(a => a.MyFavouriteGames);
        }

        public IEnumerable<NewsUser> UserWithRoles(Expression<Func<NewsUser, bool>> predicate)
        {
            return _context.Set<NewsUser>().Include(a => a.Roles).Where(predicate);
        }

        public IEnumerable<NewsUser> UserWithBlogPosts(Expression<Func<NewsUser, bool>> predicate)
        {
            return _context.Set<NewsUser>().Include(a => a.BlogPosts).Where(predicate);
        }
    }
}
