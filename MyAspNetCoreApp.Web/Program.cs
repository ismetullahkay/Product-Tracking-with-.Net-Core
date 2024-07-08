using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
});
//VT baðlantýsý tamamlandý
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddSingleton<IHelper, Helper>(); //Singleton olarak verdik.
//herhangi bir classýn construnctor veya metodunda IHelper adýnda ýnterface görürsen Helper sýnýfýndan bir nesne örneði üret

builder.Services.AddScoped<ILow, Low>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //çalýþmýþ olan assemblyi al 

builder.Services.AddScoped<BulunamadiFilter>(); //req responsa dönene kadar hepa ayný nesneyi verdiði için en doðrusu scoepd


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


#region Conventional Route iþlemi (çok kullanýlmýyor)
//app.MapControllerRoute( //bu sayede linkleme düzgün oldu.urlde direkt getbyýd/21 gibi.diðeri getbyýd?denemeid=21 yazýyodu
//    name: "productsgetbyýd",
//    pattern: "{controller=Products}/{action=GetById}/{denemeid}");

//app.MapControllerRoute(
//    name: "productspages",
//    pattern: "{controller=Products}/{action=Pages}/{pages}/{pageSize}");

//app.MapControllerRoute(
//    name: "article",
//    pattern: "{controller=Blog}/{action=Article}/{name}/{id}"); 
#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
