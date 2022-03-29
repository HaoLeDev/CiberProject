using Model.Models;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IOrdersRepository : IRepository<Order> 
    { 
    }
    public class OrdersRepository : RepositoryBase<Order>, IOrdersRepository
    {
        public OrdersRepository(CiberDbContext dbContext) : base(dbContext)
        {
        }
    }
}
