using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketTrackingSystem.Application.ExtensionMethod;
using TicketTrackingSystem.Common.Model;
using TicketTrackingSystem.Core.Model;
using TicketTrackingSystem.DAL.Implementation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TicketTrackingSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
})
.AddRoles<Role>()
.AddEntityFrameworkStores<TicketTrackingSystemDbContext>()
.AddDefaultTokenProviders()
.AddUserManager<UserManager<ApplicationUser>>()
.AddRoleManager<RoleManager<Role>>()
.AddSignInManager<SignInManager<ApplicationUser>>();
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);



builder.Services.AddAuthentication().AddCookie("Identity.Application", options =>
{
    options.Cookie.Name = "TicketTrakingSystem";
    options.Cookie.HttpOnly = true;
    //options.ExpireTimeSpan = TimeSpan.FromHours(5);
    //options.Cookie.MaxAge = options.ExpireTimeSpan;
    options.LogoutPath = new PathString("/Account/LogOut");
    options.LoginPath = new PathString("/Account/Login");
    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
    options.SlidingExpiration = true;
});




//////////////////////////////////////////


builder.Services.AddCustomServices();

//////////////////////////////////////////







builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.Use(async (context, next) =>
//{
//    var token = context.Session.GetString("jwtToken");
//    if (!string.IsNullOrEmpty(token))
//    {
//        context.Request.Headers.Add("Authorization", "Bearer " + token);
//    }
//    await next();
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
