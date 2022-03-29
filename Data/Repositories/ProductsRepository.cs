using Model.Models;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IProductsRepository : IRepository<Product>
    { 
    }
    public class ProductsRepository : RepositoryBase<Product>, IProductsRepository
    {
        public ProductsRepository(CiberDbContext dbContext) : base(dbContext)
        {
        }
    }
}
