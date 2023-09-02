using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Udemy.Identity.WebUI.Context;
using Udemy.Identity.WebUI.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppDbContext>();

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

app.Run();
