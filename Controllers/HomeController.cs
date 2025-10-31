using Microsoft.AspNetCore.Mvc;
using ManicureShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ManicureShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            var featured = await _db.Products.Take(8).ToListAsync();
            return View(featured);
        }
    }
}
