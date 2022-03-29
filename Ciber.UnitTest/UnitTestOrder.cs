using CiberProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Moq;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ciber.UnitTest
{
    public class UnitTestOrder
    {
        private Mock<ICustomersService> _customersService = new Mock<ICustomersService>();
        private Mock<IProductsService> _productsService = new Mock<IProductsService>();
        private Mock<IOrdersService> _ordersService = new Mock<IOrdersService>();

        [Fact]
        public async Task GetAllOrderUnitTest()
        {
            // Arrange

            _ordersService.Setup(repo => repo.GetAll("")).ReturnsAsync(GetAllOrders());
            var controller = new OrdersController(_ordersService.Object, _customersService.Object, _productsService.Object);

            // Act
            var result = await controller.Index("", "", 2);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Order>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private static IEnumerable<Order> GetAllOrders()
        {
            return new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    Amount = 1000,
                    CustomerId = 1,
                    OrderDate=System.DateTime.Now,
                    ProductId=1,
                },
                new Order()
                {
                    Id = 2,
                    Amount = 1500,
                    CustomerId = 2,
                    OrderDate=System.DateTime.Now,
                    ProductId=2,
                },
            };
        }
    }
}