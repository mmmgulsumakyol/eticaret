using ETicaret.Models.entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

public class Startup{
    public Startup(IConfiguration configuration){
      configuration= configuration;
    }

public void ConfigureServices(IServiceCollection services){
    const int maxRequestLimit=26214400;

    services.Configure<IISServerOptions>(options=>{
        options.MaxRequestBodySize=maxRequestLimit;
    });
    services.Configure<KestrelServerOptions>(option => {
        option.Limits.MaxRequestBodySize=maxRequestLimit;
    });
       services.Configure<FormOptions>(option => {
        option.ValueLengthLimit=maxRequestLimit;
        option.MultipartBodyLengthLimit=maxRequestLimit;
        option.MultipartHeadersLengthLimit=maxRequestLimit;
    });

    services.AddControllersWithViews ().AddRazorRuntimeCompilation();
    services.AddDbContext<EticaretDbContext>{
        Options => option.UseSql(Configuration.GetConnectionString("Default"))

        
    
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Options=>{Options.LoginPath="admin/login/;"});
}}