using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Implementation;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.Entity;
using MyWatchShop.Services.Implementation;
using MyWatchShop.Services.Interfaces;
using MyWatchShop.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<ShopDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddScoped<IRepository, Repository >();
builder.Services.AddScoped<IProductService, ProductService >();
builder.Services.AddScoped<IAdminServices, AdminServices >();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddScoped<IUploadService, UploadService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=BestSeller}/{id?}");

app.Run();
