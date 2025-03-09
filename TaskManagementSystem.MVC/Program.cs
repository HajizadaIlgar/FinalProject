using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.BL;
using TaskManagementSystem.BL.Helpers;
using TaskManagementSystem.CORE.Entities.Users;
using TaskManagementSystem.DAL.Contexts;
using TaskManagementSystem.MVC.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TaskManagementDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MySQL"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
}).AddDefaultTokenProviders().AddEntityFrameworkStores<TaskManagementDbContext>();

var opt = new SmtpOption();
builder.Services.Configure<SmtpOption>(builder.Configuration.GetSection(SmtpOption.Name));

builder.Services.AddControllersWithViews();
builder.Services.AddServices();
builder.Services.AddAutoMapper();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chatHub");
});


app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Analytics}/{id?}");

app.Run();
