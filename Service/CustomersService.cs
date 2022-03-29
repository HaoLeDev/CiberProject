using Data.Repositories;
using Model.Models;

namespace Service
{
    public interface ICustomersService
    {
        Task<IEnumerable<Customer>> GetAll();

        Task<IEnumerable<Customer>> GetAll(string keyword);

        Task<Customer> Add(Customer customer);

        Task<Customer> Update(Customer customer);

        Task<Customer> GetById(int id);

        Task<Customer> Delete(int id);

        Task<bool> CustomerExists(int id);
    }

    public class CustomersService : ICustomersService
    {
        private ICustomersRepository _customersRepository;

        public CustomersService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<Customer> Add(Customer customer)
        {
            return await _customersRepository.AddASync(customer);
        }

        public async Task<bool> CustomerExists(int id)
        {
            return await _customersRepository.CheckContainsAsync(x=>x.Id== id);
        }

        public async Task<Customer> Delete(int id)
        {
            return await _customersRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _customersRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Customer>> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return await _customersRepository.GetAllAsync(x => x.Name.ToLower().Contains(keyword));
            return await _customersRepository.GetAllAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _customersRepository.GetByIdAsync(id);
        }

        public async Task<Customer> Update(Customer customer)
        {
            return await _customersRepository.UpdateASync(customer);
        }
    }
}