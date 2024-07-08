using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents
{
    public class VisitorViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VisitorViewComponent(AppDbContext context,IMapper Mapper)
        {
            _context = context;
            _mapper = Mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
       {
        //    var visitor= _context.Visitors.ToList();
        //    var visitorViewModel=_mapper.Map<List<VisitorViewModel>>(visitor);
        //    ViewBag.visitorViewModel= visitorViewModel;
            return View();
        }
    }
}
