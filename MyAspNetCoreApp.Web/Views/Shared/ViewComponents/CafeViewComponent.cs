using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents
{
    public class CafeViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CafeViewComponent(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item=_context.Cafe.OrderBy(x => x.masaNo).ToList();
            var mapitem=_mapper.Map<List<CafeViewModel>>(item);
            ViewBag.mapitem=mapitem;
            return View();
               
        }
    }
}
