using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Connect to the database
builder.Services.AddDbContext<AspnetCoreMvcFullContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("AspnetCoreMvcFullContext") ?? throw new InvalidOperationException("Connection string 'AspnetCoreMvcFullContext' not found.")));

builder.Services.AddDbContext<DebtsyncContext>(options =>
    options.UseNpgsql("Server=localhost;Port=5433;Database=debtsync;Uid=postgres;Pwd=123;"));
//builder.Services.AddDbContext<DebtsyncContext>(options =>
//    options.UseNpgsql("Server=monorail.proxy.rlwy.net;Port=14788;Database=railway;Uid=postgres;Pwd=AAkaonmDsoevodbHWIsOzrTieOKBfczc;"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
               op =>
               {
                 op.LoginPath = "/Auth/Login";
                 op.AccessDeniedPath = "/Auth/Login";
                 op.ExpireTimeSpan = TimeSpan.FromMinutes(60);
               }
               );

var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Create a service scope to get an AspnetCoreMvcFullContext instance using DI and seed the database.
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

//app.UseHttpsRedirection();  //for production...comment out  
app.UseStaticFiles();

app.UseRouting();
//app.UseCookiePolicy();
//app.UseAuthentication();
app.UseAuthorization();
//app.UseSession();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Dashboards}/{action=myexcel}/{id?}"); // <-- Update in AspnetCoreMvcStarter
    pattern: "{controller=Auth}/{action=Login}/{id?}"); // <-- Update in AspnetCoreMvcStarter

app.Run();
