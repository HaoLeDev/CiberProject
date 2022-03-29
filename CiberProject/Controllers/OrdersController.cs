#nullable disable

using CiberProject.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Service;
using System.Collections.Generic;

namespace CiberProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService _ordersService;
        private readonly ICustomersService _customersService;
        private readonly IProductsService _productsService;

        public OrdersController(IOrdersService ordersService, ICustomersService customersService, IProductsService productsService)
        {
            _ordersService = ordersService;
            _customersService = customersService;
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _ordersService.GetAll();
        }

        // GET: Orders
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            var query = await _ordersService.GetAll(searchString);
            int pageSize = 2;
            var paging = PaginatedList<Order>.CreateAsync(query, pageNumber ?? 1, pageSize);
            return View(paging);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _ordersService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CustomerId"] = new SelectList(await _customersService.GetAll(), "Id", "Name");
            ViewData["ProductId"] = new SelectList(await _productsService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,ProductId,Amount,OrderDate")] Order order)
        {
            if (ModelState.IsValid)
            {
                await _ordersService.Add(order);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(await _customersService.GetAll(), "Id", "Name", order.CustomerId);
            ViewData["ProductId"] = new SelectList(await _productsService.GetAll(), "Id", "Name", order.ProductId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _ordersService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(await _customersService.GetAll(), "Id", "Name", order.CustomerId);
            ViewData["ProductId"] = new SelectList(await _productsService.GetAll(), "Id", "Name", order.ProductId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,ProductId,Amount,OrderDate")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ordersService.Update(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(await _customersService.GetAll(), "Id", "Name", order.CustomerId);
            ViewData["ProductId"] = new SelectList(await _productsService.GetAll(), "Id", "Name", order.ProductId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _ordersService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _ordersService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OrderExists(int id)
        {
            return await _ordersService.OrderExists(id);
        }
    }
}