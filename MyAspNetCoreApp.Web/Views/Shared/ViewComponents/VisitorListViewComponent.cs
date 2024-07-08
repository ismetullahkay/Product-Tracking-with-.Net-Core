using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents
{
    public class VisitorListViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public VisitorListViewComponent(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var visitorlist=_context.Visitors.OrderByDescending(x => x.Id).ToList();
            var visitorlistVM=_mapper.Map<List<VisitorViewModel>>(visitorlist);

            ViewBag.visitorlistVM=visitorlistVM;

            return View();
        }



    }
}
