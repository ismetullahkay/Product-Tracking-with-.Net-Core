using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Filters
{
    public class BulunamadiFilter:ActionFilterAttribute
    {
        private readonly AppDbContext _context;

        public BulunamadiFilter(AppDbContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var idValue = context.ActionArguments.Values.First(); //ilk parametresini değerini al

            var id=(int)idValue;

            var hasProduct=_context.Products.Any(x=>x.Id==id); //VT'deki id ile bu Id'yi eşitler //any bool değer alır true ya da false

            if (hasProduct == false)
            {
                context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel() { Errors=new List<string>() { $"Bu ({id})id değerine sahip ürün veritabanında bulunamadı" }  } ); 
                //home cntrl error actiona gönderir mesajı 
            }

        }
    }
}
