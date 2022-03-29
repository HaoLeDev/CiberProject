using Model.Models;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface ICategoriesRepository : IRepository<Category>
    {

    }
    public class CategoriesRepository : RepositoryBase<Category>, ICategoriesRepository
    {
        public CategoriesRepository(CiberDbContext dbContext) : base(dbContext)
        {
        }
    }
}
