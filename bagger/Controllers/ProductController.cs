using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bagger.Data;
using bagger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges(); // --> Error Solution: dotnet ef migrations add [migration name] -> dotnet ef database update
            return View();
        }
       
        public IActionResult Edit(int id)
        {
            var item = _dbContext.Products.Find(id);
            if(item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if(id != product.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
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
            if (ModelState.IsValid)
            {
                _dbContext.Remove(product);
                _dbContext.SaveChanges();
            }
            return Redirect("/Product");
            
        }
    }
}

