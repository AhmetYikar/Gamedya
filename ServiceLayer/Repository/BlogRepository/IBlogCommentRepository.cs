﻿using Entites.Models.BlogModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Repository.BlogRepository
{
    public interface IBlogCommentRepository : IRepository<BlogComment>
    {
    }
}
