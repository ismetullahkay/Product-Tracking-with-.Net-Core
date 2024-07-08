using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;
using System.Diagnostics;
using static MyAspNetCoreApp.Web.ViewModels.ProductListPartialViewModel;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ILow _low;

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger,ILow low, AppDbContext context,IMapper mapper)
        {
            _logger = logger;
            _low = low;
            _context=context;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name=x.Name,
                Price=x.Price,
                Stock=x.Stock

            }).ToList();

            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };


            return View();
        }
        [ExceptionaFilter]
        public IActionResult Privacy()
        {
           // throw new Exception("Veritabanıyla ilgili bir sorun meydana geldi !");
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {

                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock

            }).ToList();

            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products
            };


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(errorViewModel);
        }
        public IActionResult Visitor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveComment(VisitorViewModel visitorViewModel)
        {
            try
            {
                var visitor = _mapper.Map<Visitor>(visitorViewModel);
                visitor.Created= DateTime.Now;
                _context.Visitors.Add(visitor); //yorumu vt'ye ekledik
                _context.SaveChanges();
                TempData["result"] = "Yorum Başarıyla Eklenmiştir"; //TD ile bu metottan başka metoda taşıdık
                return RedirectToAction("Visitor");
            }
            catch (Exception)
            {

                TempData["result"] = "Yorum Kaydedilemedi !";
                return RedirectToAction("Visitor");
            }
        }
    }
}
//prv
//var text1 = "İSMETULLAH KAYIKÇI";
//var lowaText=_low.Lowa(text1);