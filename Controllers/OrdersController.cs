using Microsoft.AspNetCore.Mvc;
using ManicureShop.Data;
using Microsoft.EntityFrameworkCore;

namespace ManicureShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrdersController(ApplicationDbContext db) { _db = db; }

        public async Task<IActionResult> Index()
        {
            var orders = await _db.Orders.Include(o => o.Items).ThenInclude(i => i.Product).OrderByDescending(o => o.Date).ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _db.Orders.Include(o => o.Items).ThenInclude(i => i.Product).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return NotFound();
            return View(order);
        }
    }
}
