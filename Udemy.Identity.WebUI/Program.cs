using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Udemy.Identity.WebUI.Context;
using Udemy.Identity.WebUI.CustomDescriber;
using Udemy.Identity.WebUI.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 2;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    //opt.SignIn.RequireConfirmedEmail = true; 
    opt.Lockout.MaxFailedAccessAttempts = 3;
})
//}).AddErrorDescriber<CustomErrorDescriber>()
.AddEntityFrameworkStores<AppDbContext>();


builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "UdemyCookie";
    opt.ExpireTimeSpan = TimeSpan.FromDays(2);
    opt.LoginPath = new PathString("/Home/SignIn");
});

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(@"Data source=IGU-NB-0884;initial catalog=UdemyIdentityDbContext;user id=sa;password=s123456*-;");
    //opt.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
});


var app = builder.Build();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/adminpanel")),
    RequestPath = "/adminpanel"
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
