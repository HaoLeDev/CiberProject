using Model.Models;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ICustomersRepository : IRepository<Customer>
    {

    }
    public class CustomersRepository : RepositoryBase<Customer>, ICustomersRepository
    {
        public CustomersRepository(CiberDbContext dbContext) : base(dbContext)
        {
        }
    }
}
