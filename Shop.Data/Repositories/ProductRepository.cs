using System;
using System.Collections;
using System.Collections.Generic;
using Shop.Data.Infrastructure;
using Shop.Model.Models;
using System.Linq;

namespace Shop.Data.Repositories
{ 
    public interface IProductRepository
    {
       
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}