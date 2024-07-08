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
//VT ba�lant�s� tamamland�
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddSingleton<IHelper, Helper>(); //Singleton olarak verdik.
//herhangi bir class�n construnctor veya metodunda IHelper ad�nda �nterface g�r�rsen Helper s�n�f�ndan bir nesne �rne�i �ret

builder.Services.AddScoped<ILow, Low>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); //�al��m�� olan assemblyi al 

builder.Services.AddScoped<BulunamadiFilter>(); //req responsa d�nene kadar hepa ayn� nesneyi verdi�i i�in en do�rusu scoepd


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


#region Conventional Route i�lemi (�ok kullan�lm�yor)
//app.MapControllerRoute( //bu sayede linkleme d�zg�n oldu.urlde direkt getby�d/21 gibi.di�eri getby�d?denemeid=21 yaz�yodu
//    name: "productsgetby�d",
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
