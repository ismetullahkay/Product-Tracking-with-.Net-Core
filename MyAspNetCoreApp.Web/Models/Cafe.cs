namespace MyAspNetCoreApp.Web.Models
{
    public class Cafe
    {
        public int Id { get; set; }
        public int masaNo { get; set; }
        public string urun { get; set; }
        public int adet { get; set; }
        public decimal fiyat { get; set; }
        public decimal Tutar { get; set; }
        public DateTime oturmaTarih { get; set; }
    }
}
