using Microsoft.AspNetCore.Mvc;
using Tiki_Web_Project.Data;
using Microsoft.EntityFrameworkCore;
using Tiki_Web_Project.Models;

namespace Tiki_Web_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult HomePage()
        {
            var products = _context.Products
                                    .Include(p => p.SubCategory)
                                    .ThenInclude(sc => sc.Category) 
                                    .ToList();
            var categories = _context.Categories.ToList();
            var subCategories = _context.subCategories.ToList();
            var services = _context.Services.ToList();
            var quicklinks = _context.QuickLinks.ToList();
            var users = _context.Users.ToList();

            var mainViewModels = new HomeViewModel
            {
                Products = products,
                Categories = categories,
                Services = services,
                QuickLinks = quicklinks,
                Users = users
            };
            return View(mainViewModels);
        }

        public IActionResult ShowProduct_ByCategory(int id)
        {
            var products = _context.Products.ToList();
            var category = _context.Categories.FirstOrDefault(c => c.CategoryID == id);
            var users = _context.Users.ToList();
            var subCategories = _context.subCategories.Where(sc => sc.CategoryID == id).ToList();

            var mainViewModels = new HomeViewModel
            {
                Categories = category != null ? new List<Category> { category } : new List<Category>(),
                Products = products,
                Users = users,
                SubCategories = subCategories
            };
            return View(mainViewModels);
        }

    }
}
