using Data.Repositories;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetAll();

        Task<IEnumerable<Product>> GetAll(string keyword);

        Task<Product> Add(Product product);

        Task<Product> Update(Product product);

        Task<Product> GetById(int id);

        Task<Product> Delete(int id);

        Task<bool> ProductExists(int id);
    }

    public class ProductsService : IProductsService
    {
        private IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<Product> Add(Product Product)
        {
            return await _productsRepository.AddASync(Product);
        }

        public async Task<Product> Delete(int id)
        {
            return await _productsRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productsRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return await _productsRepository.GetAllAsync(x => x.Name.ToLower().Contains(keyword) || x.Description!.Contains(keyword));
            return await _productsRepository.GetAllAsync();
        }

        public Task<Product> GetById(int id)
        {
            return _productsRepository.GetByIdAsync(id);
        }

        public async Task<bool> ProductExists(int id)
        {
            return await _productsRepository.CheckContainsAsync(x=>x.Id==id);
        }

        public async Task<Product> Update(Product Product)
        {
            return await _productsRepository.UpdateASync(Product);
        }
    }
}
