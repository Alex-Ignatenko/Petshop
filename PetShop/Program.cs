using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Repositories;
using PetShop.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRepository, PetShopRepository>();
builder.Services.AddSingleton<IPhotoService, PhotoService> ();
builder.Services.AddSingleton<ISelectListService, SelectListService> ();

string connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"]!;
builder.Services.AddDbContext<PetShopContext>(
    options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();


app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<PetShopContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

app.UseRouting();
app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{Id?}");

app.Run();