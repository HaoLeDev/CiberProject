#nullable disable

using CiberProject.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Service;

namespace CiberProject.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ICategoriesService _categoriesService;

        public ProductsController(IProductsService productsService, ICategoriesService categoriesService)
        {
            _productsService = productsService;
            _categoriesService = categoriesService;
        }

        // GET: Products
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
            var query = await _productsService.GetAll(searchString);
            int pageSize = 2;
            var paging = PaginatedList<Product>.CreateAsync(query, pageNumber ?? 1, pageSize);
            return View(paging);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productsService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoriesService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,Name,Price,Description,Quantity")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _productsService.Add(product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Serilog.Log.Error("Error: POST: Products/Create Error:", ModelState);
            }
            ViewData["CategoryId"] = new SelectList(await _productsService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productsService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _productsService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,Name,Price,Description,Quantity")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productsService.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
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
            else
            {
                Serilog.Log.Error("Error: POST: Products/Edit/5 Error:", ModelState);
            }

            ViewData["CategoryId"] = new SelectList(await _productsService.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productsService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _productsService.ProductExists(id);
        }
    }
}