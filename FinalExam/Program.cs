using FinalExam.DAL;
using FinalExam.Models;
using FinalExam.Services;
//using FinalExamLumia.DAL;
//using FinalExamLumia.Models;
//using FinalExamLumia.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<Layoutservice>();
builder.Services.AddScoped<HomeService>();
builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.LoginPath = $"/Admin/Account/Login/{cfg.ReturnUrlParameter}";
});
var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    "default",
    "{area:exists}/{controller=home}/{action=index}/{id?}"
    );
app.MapControllerRoute(
    "default",
    "{controller=home}/{action=index}/{id?}"
    );

app.Run();
