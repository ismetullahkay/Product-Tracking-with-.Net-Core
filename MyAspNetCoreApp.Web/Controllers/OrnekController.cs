using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Filters;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class Product2
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class OrnekController : Controller
    {
        [ResultaFilter("Deneme","nice deneme2")]
        public IActionResult Index0()
        {
            var ProductList=new List<Product2>()
            {
                new Product2 { Id = 1,Name="kulaklık"},
                new Product2 { Id = 2,Name="keyboard"},
                new Product2 { Id = 4,Name="mouse"},
                new Product2 { Id = 3,Name="notebook stand"}
            };
            return View(ProductList);
        }


        public IActionResult Index() 
        {
            ViewBag.isim = "İSMETULLAH KAYIKÇI";
            ViewBag.yas = 24;
            ViewBag.sehir = "Bucak";
            ViewBag.KisiBilgi = new List<string>() { "İSMETULLAH KAYIKÇI", "24", "Bucak" };


            ViewData["isim"] = "İrem Karaca";
            ViewData["age"] = 22;
            ViewData["PersonInformation"] = new List<string>() { "İrem Karaca", "22" };


            TempData["KulaklıkMarka"] = "HAYLOU MORİPODS V5.2";


            return View();
        }
        
        public IActionResult Index2()
        {
            return RedirectToAction("Index","Ornek"); //Ornek Controllerdaki Index action metoduna yönlendirdik.
            //Aynı sayfada olduğundan "ornek" yazmaya gerek yoktu.default olarak ornek alır
        }
        public IActionResult Index3()
        {
            return View();
        }



        public IActionResult ContentResult() 
        {
            return Content("Content Result sadece string bir sayfa döndürür.");

        }
        public IActionResult JsonResult() 
        {
            return Json(new { Id = 1, name = "kalem ", price = 100 });

        }
        public IActionResult EmptyResult() 
        {
            return new EmptyResult();
        }
    }
}
