using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class CafeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CafeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        { 
            return View();
        }
        public IActionResult Add()
        {
            ViewBag.selectlist = new SelectList(new List<CafeSelectList>()
            {
                new CafeSelectList(){Data="Kola",Value="Kola"},
                new CafeSelectList(){Data="Kahve",Value="Kahve"},
                new CafeSelectList(){Data="Çay",Value="Çay"},
                new CafeSelectList(){Data="Salep",Value="Salep"}
            }, "Value", "Data");

            return View();
        }
        [HttpPost]
        public IActionResult Add(CafeViewModel cafeViewModel)
        {
            

            var urun = _mapper.Map<Cafe>(cafeViewModel);

            urun.oturmaTarih=DateTime.Now;

           



            _context.Cafe.Add(urun);
            _context.SaveChanges(); 
            return RedirectToAction("Index");
        
        }
        public IActionResult Remove(int id)
        {
            var urun=_context.Cafe.Find(id);
            urun.urun = " ";
            urun.fiyat = 0;
            urun.adet = 0;
            urun.Tutar = 0;
            urun.oturmaTarih = DateTime.MinValue;
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var urun = _context.Cafe.Find(id);
            _context.Cafe.Remove(urun);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult UrunGir(int id) 
        {
            var urun = _context.Cafe.Find(id);

            ViewBag.selectlist = new SelectList(new List<CafeSelectList>()
            {
                new CafeSelectList(){Data="Kola",Value="Kola"},
                new CafeSelectList(){Data="Kahve",Value="Kahve"},
                new CafeSelectList(){Data="Çay",Value="Çay"},
                new CafeSelectList(){Data="Salep",Value="Salep"}
            }, "Value", "Data",urun.urun);

            
            return View(_mapper.Map<CafeViewModel>(urun));
        }
        [HttpPost]
        public IActionResult UrunGir(CafeViewModel cafe)
        {
            var urun = _mapper.Map<Cafe>(cafe);
            urun.oturmaTarih=DateTime.Now;
            _context.Cafe.Update(urun);
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
