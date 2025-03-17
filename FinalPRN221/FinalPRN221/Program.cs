using FinalPRN221.Extensions;
using FinalPRN221.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Thêm DbContext identity
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});
// Cấu hình Serilog để sử dụng bảng mặc định `Logs`
Serilog.Log.Logger = new LoggerConfiguration()
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "Logs",  // 📌 Đúng với bảng mặc định của Serilog
            AutoCreateSqlTable = true // 📌 Cho phép Serilog tự tạo bảng nếu chưa có
        }
    )
    .CreateLogger();

builder.Host.UseSerilog(); // Đặt Serilog làm logger mặc định

// Thêm services MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
//tao role cho bang identityrole
var scope = app.Services.CreateScope();
await GenerateRoles.EnsureRoles(scope.ServiceProvider);
app.UseStaticFiles();
app.UseRouting();
// Cấu hình Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages(); // su dung trang authen mac dinh cua identity
app.MapDefaultControllerRoute();

app.Run();