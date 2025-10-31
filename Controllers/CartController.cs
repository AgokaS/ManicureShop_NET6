using Microsoft.AspNetCore.Mvc;
using ManicureShop.Data;
using ManicureShop.Models;
using System.Text.Json;

namespace ManicureShop.Controllers
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var str = session.GetString(key);
            return str == null ? default : JsonSerializer.Deserialize<T>(str);
        }
    }

    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const string SessionCartKey = "Cart";

        public CartController(ApplicationDbContext db) { _db = db; }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int qty = 1)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObject<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();
            var existing = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existing != null) existing.Quantity += qty;
            else cart.Add(new CartItem { ProductId = product.Id, ProductName = product.Name, Price = product.Price, Quantity = qty, ImagePath = product.ImagePath });

            HttpContext.Session.SetObject(SessionCartKey, cart);
            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null) cart.Remove(item);
            HttpContext.Session.SetObject(SessionCartKey, cart);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int qty)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (qty <= 0) cart.Remove(item);
                else item.Quantity = qty;
            }
            HttpContext.Session.SetObject(SessionCartKey, cart);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(string? userId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();
            if (!cart.Any()) return RedirectToAction(nameof(Index));

            if (string.IsNullOrEmpty(userId))
                userId = User.Identity?.Name ?? "guest";

            var order = new Order { UserId = userId, Date = DateTime.UtcNow, TotalPrice = cart.Sum(c => c.Price * c.Quantity), Items = new List<OrderItem>() };
            foreach (var c in cart)
            {
                order.Items.Add(new OrderItem { ProductId = c.ProductId, Quantity = c.Quantity, Price = c.Price });
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
            HttpContext.Session.Remove(SessionCartKey);
            return RedirectToAction("Index", "Orders");
        }
    }
}
