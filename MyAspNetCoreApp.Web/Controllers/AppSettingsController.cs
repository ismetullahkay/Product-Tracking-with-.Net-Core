using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class AppSettingsController : Controller
    {
        private readonly IConfiguration _configuration;

        public AppSettingsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.baseUrl = _configuration["baseUrl"];
            ViewBag.smsKey = _configuration["Keys:smsKey"];
            ViewBag.emailKey = _configuration.GetSection("Keys")["emailKey"];

            return View();
        }
    }
}
