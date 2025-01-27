using Hospital.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers(); // API Controller deste�i ekleniyor.

// Add DbContext
builder.Services.AddDbContext<HospitalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum s�resi
    options.Cookie.HttpOnly = true; // G�venlik i�in sadece HTTP �zerinden eri�im
    options.Cookie.IsEssential = true; // GDPR uyumlulu�u i�in gerekli
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // HSTS g�venlik ba�l���
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

// API Route'lar�n� desteklemek i�in ekleme
app.MapControllers();

app.Run();
