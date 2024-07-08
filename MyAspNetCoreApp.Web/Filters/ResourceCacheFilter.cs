using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAspNetCoreApp.Web.Filters
{
    public class ResourceCacheFilter : Attribute, IResourceFilter
    {
        private static IActionResult _cache;
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _cache = context.Result; //cache ya vt listesini attığımız için her seferinde listeyi çekmek yerine catch nesnesini döner
                                     //böylece çok çabuk şekilde listeleme olur.çümkü listeleme 1 kere üretilip cache verilir
                                     //o da null olmadığı için her seferinde aynı listeyi döndürecektir
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if(_cache != null)
            {
                context.Result = _cache;
            }
        }
    }
}
