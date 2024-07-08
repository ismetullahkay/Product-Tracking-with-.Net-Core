using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Filters
{
    public class ExceptionaFilter:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true; //hata aldık

             var error =context.Exception.Message; //hata mesajını aldık

            context.Result = new RedirectToActionResult("Error", "Home", new ErrorViewModel() { Errors = new List<string>() {$"{error}" } });

            //hatayı gönderdilk homedaki errora 
        }
    }
}
