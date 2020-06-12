using DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.RoleRepository
{
    public class RoleRepository : GenericRepository<IdentityRole>, IRoleRepository
    {
        public RoleRepository(GameNewsDbContext context)
       : base(context)
        {

        }

        public GameNewsDbContext context { get { return _context as GameNewsDbContext; } }

       
    }

}
