using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModels;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _productRepository;

        private AppDbContext _context;

        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;

        public ProductsController(AppDbContext context, IMapper mapper, IFileProvider fileProvider)
        {
            _productRepository = new ProductRepository();
            //ProRepo'ya erişmek için ProREPO nesnesine eriştik constr sayesinde.(sınıfın kopyasını verdik gibi)
            _context = context;

            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        //[ResourceCacheFilter]
        public IActionResult Index()
        {

            List<ProductViewModel>products=_context.Products.Include(x=>x.Category).Select(x=> new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CategoryName=x.Category.Name,
                Price = x.Price,
                Stock = x.Stock,
                Color = x.Color,
                Expire=x.Expire,
                ImagePath = x.ImagePath,
                isPublished=x.isPublished,
                publishDate=x.publishDate,
               
            }).ToList();
           
            //var products = _context.Products.ToList();
            return View(products) ;
            //_mapper.Map<List<ProductViewModel>>(products)
        }

        [ServiceFilter(typeof(BulunamadiFilter))]
        [Route("[controller]/[action]/{denemeid}",Name ="getbyID")] 
        public IActionResult GetById(int denemeid)
        {
            var product = _context.Products.Find(denemeid); //id'ye göre bulmak için id'yi aldık
            return View(_mapper.Map<ProductViewModel>(product));
        }




        [Route("controlleraistediğimiz/actionaistediğimizİsmiverebiliriz/{page}/{pageSize}",Name ="Pages")]
        //bu şekilde özelleştirdik.hiç isim vermeyedebilirdik
        public IActionResult Pages(int page,int pageSize)
        {
            //ilk 3 kaydı sonraki 3 kaydı şeklinde verdik .1-3
            //2-3
            //3-3
            var products = _context.Products.Skip((page-1)*pageSize).Take(pageSize).ToList();


            ViewBag.page = page;
            ViewBag.pageSize = pageSize;
            return View(_mapper.Map<List<ProductViewModel>>(products));
        }






        [ServiceFilter(typeof(BulunamadiFilter))]
        public IActionResult RemoveProduct(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();

            TempData["status"] = "Ürün Başarıyla Silindi";
            return RedirectToAction("Index");
        }






        [HttpGet]
        public IActionResult AddProduct()  
        {  
            ViewBag.Expire=new Dictionary<string, int>()
            {
                {"1 ay",1 },
                {"3 ay",3 },
                {"6 ay",6 },
                {"12 ay",12 }
            };

            ViewBag.SelectColor = new SelectList(new List<ColorSelectList>() {
                new (){Data="Mavi",Value="Mavi"},
                new (){Data="Sarı",Value="Sarı"},
                new (){Data="Kırmızı",Value="Kırmızı"}


            },"Value","Data");

            var categories=_context.Category.ToList();

            ViewBag.SelectCategories = new SelectList(categories, "Id", "Name"); //Id vt de tutulur ,name kullanıcıya gösterilir

            

            return View();
        }








        [HttpPost]
        public IActionResult AddProduct(ProductViewModel newProduct)
        {
            IActionResult result = null;

           

            if (ModelState.IsValid) //doğrulama işlemi gerçekleştiyse eklenir 
            {
                try
                {
                    

                    if (newProduct.Image !=null && newProduct.Image.Length > 0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot"); //"" olsa kök ,"wwwroot" olsa bu klasordeyiz

                        var images = root.First(x => x.Name == "images"); //imaages klasorunu elde ettik

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName); // random dosya adı +foto uzantısı aldık 

                        var path = Path.Combine(images.PhysicalPath, randomImageName); //resmin yolunu ve adını aldık

                        using var stream = new FileStream(path, FileMode.Create); // bu yola(path'e) kaydet.bu path'te bu dosya yoksa olustur

                        newProduct.Image.CopyTo(stream);

                        newProduct.ImagePath = randomImageName;
                        //vt'ye image yolunu gelen dosya adından kaydettik 
                    }
                    var product = _mapper.Map<Product>(newProduct);
                    _context.Products.Add(product);

                    _context.SaveChanges();

                    TempData["status"] = "Ürün Başarıyla Eklendi";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Hata oluştu. Lütfen daha sonra tekrar deneyin." + ex.InnerException.Message);
                    TempData["Error"] = ex.Message; // İsteğe bağlı: hata ayrıntılarını kaydedin
                    result = View();
                }
               
            }
            else
            {
                 
                result= View();
            }
            var categories = _context.Category.ToList();

            ViewBag.SelectCategories = new SelectList(categories, "Id", "Name");

            ViewBag.Expire = new Dictionary<string, int>()
                {
                    {"1 ay",1 },
                    {"3 ay",3 },
                    {"6 ay",6 },
                    {"12 ay",12 }
                };

            ViewBag.SelectColor = new SelectList(new List<ColorSelectList>()
                {
                    new (){Data="Mavi",Value="Mavi"},
                    new (){Data="Sarı",Value="Sarı"},
                    new (){Data="Kırmızı",Value="Kırmızı"}


                }, "Value", "Data");
            return result;

        }










        [ServiceFilter(typeof(BulunamadiFilter))]
        public IActionResult UpdateProduct(int id)  
        { 
            var product=_context.Products.Find(id);

            var categories = _context.Category.ToList();

            ViewBag.SelectCategories = new SelectList(categories, "Id", "Name");

            ViewBag.SelectExpire=product.Expire;
            ViewBag.Expire = new Dictionary<string, int>()
            {
                {"1 ay",1 },
                {"3 ay",3 },
                {"6 ay",6 },
                {"12 ay",12 }
            };

            ViewBag.SelectColor = new SelectList(new List<ColorSelectList>() {
                new (){Data="Mavi",Value="Mavi"},
                new (){Data="Sarı",Value="Sarı"},
                new (){Data="Kırmızı",Value="Kırmızı"}


            }, "Value", "Data",product.Color); //Vt^den gelen renk bilgisini de gösterdik 
           

            return View(_mapper.Map<ProductUpdateViewModel>(product)); //bu şekilde modelin içerisine ilgili ürünün id değerine sahip tüm özellikler gider
        }










        [HttpPost]
        public IActionResult UpdateProduct(ProductUpdateViewModel updateProduct)
        {

            //if(!ModelState.IsValid)
            //{
            //    ViewBag.SelectExpire = updateProduct.Expire;
            //    ViewBag.Expire = new Dictionary<string, int>()
            //{
            //    {"1 ay",1 },
            //    {"3 ay",3 },
            //    {"6 ay",6 },
            //    {"12 ay",12 }
            //};

            //    ViewBag.SelectColor = new SelectList(new List<ColorSelectList>() {
            //    new (){Data="Mavi",Value="Mavi"},
            //    new (){Data="Sarı",Value="Sarı"},
            //    new (){Data="Kırmızı",Value="Kırmızı"}


            //}, "Value", "Data", updateProduct.Color);

            //    return View();
            //}
            var categories = _context.Category.ToList();

            ViewBag.SelectCategories = new SelectList(categories, "CategoryId", "CategoryName");

            if (updateProduct.Image != null && updateProduct.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot"); //"" olsa kök ,"wwwroot" olsa bu klasordeyiz

                var images = root.First(x => x.Name == "images"); //imaages klasorunu elde ettik

                var randomImageName = Guid.NewGuid() + Path.GetExtension(updateProduct.Image.FileName); // random dosya adı +foto uzantısı aldık 

                var path = Path.Combine(images.PhysicalPath, randomImageName); //resmin yolunu ve adını aldık

                using var stream = new FileStream(path, FileMode.Create); // bu yola(path'e) kaydet.bu path'te bu dosya yoksa olustur

                updateProduct.Image.CopyTo(stream);

                updateProduct.ImagePath = randomImageName;
                //vt'ye image yolunu gelen dosya adından kaydettik 
            }



            var product = _mapper.Map<Product>(updateProduct);

            _context.Products.Update(product);
            _context.SaveChanges();

            TempData["status"] = "Ürün Başarıyla Güncellendi";
            
            return RedirectToAction("Index");
        }







        
        [AcceptVerbs("POST","GET")]
        public IActionResult HasProductName(string Name)
        {
            var product=_context.Products.Any(x => x.Name.ToLower() == Name.ToLower());

            if (product)
            {
                return Json("Bu ürün ismi zaten veritabanında bulunmaktadır. ");
            }
            else
            {
                return Json(true);
            }
        }
    }
}


#region kullanılmayan kod blokları
//if (!string.IsNullOrEmpty(newProduct.Name) && newProduct.Name.StartsWith("a"))
//{
//    ModelState.AddModelError(String.Empty, "Ürün a harfiyle başlayamaz");
//}

//if (_productRepository.GetAll().Count<1) //Repoda veri yoksa ekler.Listede eleman 0 sa
//{
//    _productRepository.Add(new Product { Id = 1, Name = "kalem1", Price = 10, Stock = 35 });
//    _productRepository.Add(new Product { Id = 2, Name = "kalem2", Price = 100, Stock = 50 });
//    _productRepository.Add(new Product { Id = 3, Name = "kalem3", Price = 1000, Stock = 70 });
//    _productRepository.Add(new Product { Id = 4, Name = "kalem4", Price = 1000, Stock = 3 });
//} Bu kodu proReposta yazacaz.çünkü tüm veriler silindikten sonra if yüzünden tekrar ekleniyor
//if (!_productRepository.GetAll().Any()) any veri yoksa false döndürür ! ile true olur




//if (!_context.Products.Any())
//{
//    _context.Products.Add(new Product { Name = "KalemDb", Price = 12, Stock = 55,Color="Lacivert"});
//    _context.Products.Add(new Product { Name = "SilgiDb", Price = 10, Stock = 70,Color="Sarı"});
//    _context.Products.Add(new Product { Name = "AçacakDb", Price = 8, Stock = 100,Color="Beyaz"});
//    _context.Products.Add(new Product { Name = "AçacaksDb", Price =28, Stock = 100, Color = "Beyaz"});

//    _context.SaveChanges(); 
//    //yapılan değişiklikleri vtye kaydeder.diğer türlü bellekte saklanır

//}


//var products=_productRepository.GetAll();
//prorepo'daki tüm verileri GETALL ile products'a attık
//Bu yöntem verileri bellekte tutar.Yeni yöntem ile DB de tutacaz



//Yöntem 1(parametresiz yöntem,Form üzerinden verileri alır)
//var name  = HttpContext.Request.Form["Name"].ToString();
//var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
//var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
//var color = HttpContext.Request.Form["Color"].ToString();

//Product newProduct= new Product() { Name=name,Price=price,Color=color,Stock=stock};
//_context.Products.Add(newProduct);

//2.Yöntem(parametre ile veriler alınır)

//public IActionResult AddProduct(string Name, decimal Price, int Stock, string Color)

//Product newProduct=new Product() { Name=Name,Price=Price,Stock=Stock,Color=Color};
//_context.Products.Add(newProduct);


//private IHelper _helper; ,IHelper helper  _helper=helper; 
//var upperText=_helper.Upper(text);

//var status = _helper.Equals(helper2); 
//[FromServices]IHelper helper2 
#endregion