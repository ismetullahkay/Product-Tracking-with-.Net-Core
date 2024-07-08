using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
            //miras aldığımız dbcon'a base ile optionsımızı gönderiyoruz 
        {
            
        }
        public DbSet<Product> Products { get; set; } 
        //Product sınıfında,Products tablosuna(sqlde) karşılık gelir.Tablodaki isimle aynı olması lazım

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<Cafe> Cafe { get; set; }

        public DbSet<Category> Category { get; set; } 

    }
}
