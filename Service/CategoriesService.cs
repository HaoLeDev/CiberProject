using Data.Repositories;
using Model.Models;

namespace Service
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAll();

        Task<IEnumerable<Category>> GetAll(string keyword);

        Task<Category> Add(Category category);

        Task<Category> Update(Category category);

        Task<Category> GetById(int id);

        Task<Category> Delete(int id);

        Task<bool> CategoryExists(int id);
    }

    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository _categorysRepository;

        public CategoriesService(ICategoriesRepository categorysRepository)
        {
            _categorysRepository = categorysRepository;
        }

        public async Task<Category> Add(Category category)
        {
            return await _categorysRepository.AddASync(category);
        }

        public async Task<bool> CategoryExists(int id)
        {
            return await _categorysRepository.CheckContainsAsync(x=>x.Id == id);
        }

        public async Task<Category> Delete(int id)
        {
            return await _categorysRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categorysRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return await _categorysRepository.GetAllAsync(x => x.Name.ToLower().Contains(keyword) || x.Description.ToLower().Contains(keyword));
            return await _categorysRepository.GetAllAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _categorysRepository.GetByIdAsync(id);
        }

        public async Task<Category> Update(Category category)
        {
            return await _categorysRepository.UpdateASync(category);
        }
    }
}