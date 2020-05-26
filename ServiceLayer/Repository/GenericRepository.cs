using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository
{
    [Serializable]
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly GameNewsDbContext _context;

        public GenericRepository(GameNewsDbContext context)
        {
            _context = context;
        }



        public IEnumerable<TEntity> GetAll()
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _context.Set<TEntity>();
        }
        public TEntity GetById(int? id)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _context.Set<TEntity>().Find(id);
        }


        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            return _context.Set<TEntity>().Where(predicate);
        }

        public void Insert(TEntity entity)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Configuration.ProxyCreationEnabled = false;

            _context.Entry(entity).State = EntityState.Modified;
        
            
        }

        public void Delete(TEntity entity)
        {
            _context.Configuration.ProxyCreationEnabled = false;
            _context.Set<TEntity>().Remove(entity);
        }


    }
}
