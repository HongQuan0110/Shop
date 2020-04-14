using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using Shop.Data.Infrastructure;
using Shop.Model.Models;

namespace Shop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}