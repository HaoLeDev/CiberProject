using Data.Repositories;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> GetAll();

        Task<IEnumerable<Order>> GetAll(string keyword);

        Task<Order> Add(Order order);

        Task<Order> Update(Order order);

        Task<Order> GetById(int id);

        Task<Order> Delete(int id);

        Task<bool> OrderExists(int id);
    }

    public class OrdersService : IOrdersService
    {
        private IOrdersRepository _ordersRepository;

        public OrdersService(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Order> Add(Order order)
        {
            return await _ordersRepository.AddASync(order);
        }

        public async Task<Order> Delete(int id)
        {
            return await _ordersRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _ordersRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Order>> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return await _ordersRepository.GetAllAsync(x => x.Amount.ToString().ToLower().Contains(keyword)||x.OrderDate.ToString().Contains(keyword));
            return await _ordersRepository.GetAllAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await _ordersRepository.GetByIdAsync(id);
        }

        public async Task<bool> OrderExists(int id)
        {
            return await _ordersRepository.CheckContainsAsync(x => x.Id == id);
        }

        public async Task<Order> Update(Order Order)
        {
            return await _ordersRepository.UpdateASync(Order);
        }
    }
}
