using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ManicureShop.Data;
using ManicureShop.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                       "Data Source=manicureshop.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Добавляем Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Добавляем MVC сервисы
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Настройка маршрутов
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Создаем базу данных и добавляем тестовые данные
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    
    // Добавляем тестовые данные если таблица Products пуста
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product { Name = "Классический маникюр", Description = "Профессиональный классический маникюр с обработкой кутикулы", Price = 1000, ImagePath = "/images/no-image.png" },
            new Product { Name = "SPA маникюр", Description = "Расслабляющий SPA маникюр с уходом и парафинотерапией", Price = 1500, ImagePath = "/images/no-image.png" },
            new Product { Name = "Гель-лак покрытие", Description = "Стойкое покрытие гель-лаком с базой и топом", Price = 1200, ImagePath = "/images/no-image.png" },
            new Product { Name = "Дизайн ногтей", Description = "Художественный дизайн ногтей с росписью", Price = 800, ImagePath = "/images/no-image.png" },
            new Product { Name = "Укрепление ногтей", Description = "Укрепление ногтей био-гелем", Price = 900, ImagePath = "/images/no-image.png" },
            new Product { Name = "Ремонт ногтя", Description = "Ремонт сломанного ногтя", Price = 300, ImagePath = "/images/no-image.png" },
            new Product { Name = "Парафинотерапия", Description = "Уход за кожей рук с парафином", Price = 700, ImagePath = "/images/no-image.png" },
            new Product { Name = "Массаж рук", Description = "Расслабляющий массаж кистей и предплечий", Price = 600, ImagePath = "/images/no-image.png" }
        );
        await db.SaveChangesAsync();
        Console.WriteLine("✅ Тестовые данные добавлены в базу");
    }
}

app.Run("http://localhost:5000");