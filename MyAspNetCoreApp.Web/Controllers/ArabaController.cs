using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ArabaController : Controller
    {
        private readonly ArabaIslem _arabaIslem;

        public ArabaController()
        {
            _arabaIslem = new ArabaIslem();
        }

        public IActionResult Index()
        {
            var arabalar = _arabaIslem.TumArabalar();
            return View(arabalar);
        }
        public IActionResult Sil(int id)
        {
            _arabaIslem.Sil(id);
            return RedirectToAction("Index");
        }
    }

}
