using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class BlogController : Controller
    {
        
        public IActionResult Article(string name,int id)
        {

            return View();
        }
    }
}
