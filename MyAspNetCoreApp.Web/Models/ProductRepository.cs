namespace MyAspNetCoreApp.Web.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>()
        {
             new Product { Id = 1, Name = "kalem1", Price = 10, Stock = 35 },
             new Product { Id = 2, Name = "kalem2", Price = 100, Stock = 50 },
             new Product { Id = 3, Name = "kalem3", Price = 1000, Stock = 70 },
             new Product { Id = 4, Name = "kalem4", Price = 1000, Stock = 3 }
        };

    public List<Product> GetAll() => _products; //metodu dış dünyaya açtık yani diğer sayfalarda çağırabiliriz.public

        public void Add(Product newProduct)=>_products.Add(newProduct);
        //Bu metod Product sınıfından bir nesneyi parametre olarak alır.Daha sonra products listesine bu nesneyi ekleyerek döndürür.

        public void Remove(int id)
        {
            var hasProduct=_products.FirstOrDefault(x=>x.Id == id); //O id'e sahip ürünün olup olmadıgını kontrol etmek
            //ilk olarak x'1 id değerini alır kontrol eder yoksa id'2 olanı yoksa 3....diye kontrol eder yoksa null döndürür ve hata mesajı alır

            if (hasProduct == null)
            {
                throw new Exception($"Bu ID değerine sahip({id}) ürün bulunamamaktadır."); //$ anlamı o id değerini bulup ekranda gösterecek
            }
            _products.Remove(hasProduct);
            //products listesinden sil ,neyi sil? hasProduct'taki id değerine sahip veriyi
        }

        public void Update(Product updateProduct)
        {
            var hasUpdateProduct = _products.FirstOrDefault(x => x.Id == updateProduct.Id); 

            if (hasUpdateProduct == null)
            {
                throw new Exception($"Bu ID değerine sahip({updateProduct.Id}) ürün bulunamamaktadır.");
            }
            hasUpdateProduct.Name = updateProduct.Name;
            hasUpdateProduct.Price = updateProduct.Price;
            hasUpdateProduct.Stock = updateProduct.Stock;

            var index=_products.FindIndex(x=>x.Id==updateProduct.Id);

            _products[index] = hasUpdateProduct;

        }

        //=> ifadesi return _products ile aynı anlama gelir.Daha kısa yazmak için kullanılır 
        //yani şunla aynıdır
        //public List<Product> Products()
        //{
        //return _products;
        //}



    }
}
