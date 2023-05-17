using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bagger.Data;
using bagger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bagger.Controllers
{

    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: /<cßontroller>/
        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            return View(products);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges(); // --> Error Solution: dotnet ef migrations add [migration name] -> dotnet ef database update
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var item = _dbContext.Products.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _dbContext.Update(product);
                _dbContext.SaveChanges();

            }

            return Redirect("/Product");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            _dbContext.Remove(product);
            _dbContext.SaveChanges();
            return Redirect("/Product");

        }
    }


    public class WarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Warehouse
        public async Task<IActionResult> Index()
        {
            return View(await _context.Warehouses.ToListAsync());
        }

        // GET: Warehouse/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var warehouse = await _context.Warehouses.Include(w => w.Products)
        //        .FirstOrDefaultAsync(m => m.WarehouseId == id);
        //    if (warehouse == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(warehouse);
        //}

        // GET: Warehouse/Create
        public IActionResult Create()
        {
            var products = _context.Products.ToList();

            // Store the products in the ViewBag
            ViewBag.Products = products;
            return View();
        }

        // POST: Warehouse/Create
[HttpPost]
public async Task<IActionResult> Create(Warehouse warehouse, int[] productIds)
{
    if (ModelState.IsValid)
    {
        _context.Add(warehouse);
        await _context.SaveChangesAsync();

        if (productIds != null)
        {
            foreach (int productId in productIds)
            {
                var product = await _context.Products.FindAsync(productId);
                if (product != null)
                {
                    var warehouseProduct = new WarehouseProduct
                    {
                        WarehouseId = warehouse.WarehouseId,
                        ProductId = product.Id
                    };

                    _context.Add(warehouseProduct);
                }
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Retrieve the list of products again to update the ViewBag
    var products = _context.Products.ToList();
    ViewBag.Products = products;

    return View(warehouse);
}





        // GET: Warehouse/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }
        private bool WarehouseExists(int id)
        {
            return _context.Warehouses.Any(w => w.WarehouseId == id);
        }

        // POST: Warehouse/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WarehouseId,WarehouseName")] Warehouse warehouse)
        {
            if (id != warehouse.WarehouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(warehouse.WarehouseId))
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
            return View(warehouse);
        }

        // GET: Warehouse/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _context.Warehouses
                .FirstOrDefaultAsync(m => m.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }
        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warehouse = await _context.Warehouses.FindAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

    }
}