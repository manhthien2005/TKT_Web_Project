using Microsoft.AspNetCore.Mvc;
using Tiki_Web_Project.Data;
using Microsoft.EntityFrameworkCore;
using Tiki_Web_Project.Models;

namespace Tiki_Web_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(string id)
        {
            var product = _context.Products
                    .Include(p => p.SubCategory)
                    .ThenInclude(sc => sc.Category) 
                    .Include(p => p.Images)
                    .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            var productAttributes = _context.ProductAttributes
                .Where(pa => pa.ProductID == product.ProductID)
                .ToList();

            var viewModel = new HomeViewModel
            {
                Products = new List<Product> { product },
                ProductAttributes = productAttributes
            };

            return View(viewModel);
        }

    }
}
