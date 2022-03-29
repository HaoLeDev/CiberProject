using Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Service;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var path = Directory.GetCurrentDirectory();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File($"{path}\\Logs/app-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CiberDbContext>(options => options
                    .UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddScoped<CiberDbContext>();
builder.Services.AddScoped<ICategoriesRepository,CategoriesRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ICustomersService, CustomersService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();

builder.Services.AddHttpContextAccessor(); //Requierd Service for Loupe


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
