using System;
using System.Collections;
using System.Collections.Generic;
using Shop.Data.Infrastructure;
using Shop.Model.Models;
using System.Linq;

namespace Shop.Data.Repositories
{
    public interface IPostRepository
    {

    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}