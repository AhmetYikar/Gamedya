using DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using ServiceLayer.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameAdmin.Helper
{
    public static class RoleHelper
    {





        public static List<IdentityRole> GetRoles(string text)
        {
            UnitOfWork uow = new UnitOfWork(new GameNewsDbContext());

            List<IdentityRole> roles = uow.Role.GetAll().ToList();


            return roles;
        }
    }
}