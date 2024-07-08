using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;
using System.Linq;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class VisitorAjaxController : Controller
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public VisitorAjaxController(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
      
public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            var visitor = _mapper.Map<Visitor>(visitorViewModel);
            visitor.Created = DateTime.Now;
            _context.Visitors.Add(visitor); //yorumu vt'ye ekledik
            _context.SaveChanges();

            return Json(new { IsSuccess = "true" });
        }
        [HttpGet]
        public IActionResult VisitorCommentList()
        {
            var visitorlist = _context.Visitors.OrderByDescending(x => x.Id).ToList();
            var visitorlistVM = _mapper.Map<List<VisitorViewModel>>(visitorlist);

            return Json(visitorlistVM);
        }


    }
}
