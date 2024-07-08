using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }


        [Remote(action:"HasProductName",controller:"Products")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="İsim alanına '3-50' karakterden fazla girilemez !")]
        [Required(ErrorMessage="İsim alanı boş olamaz !")]
        public string Name { get; set; }



        [Required(ErrorMessage = "Fiyat alanı boş olamaz !")]
        [Range(1,1000, ErrorMessage ="Fiyat Bilgisi 1000'den büyük olamaz")]
        public decimal? Price { get; set; } //value typlar nullaable boş oldugunde dafult değer 0 alır.required ile çalışınca nb olmalı 




        [Required(ErrorMessage = "Stok alanı boş olamaz !")]
        [Range(1,1000, ErrorMessage ="Stok Miktarı 200'den büyük olamaz !")]
        public int? Stock { get; set; }




        [Required(ErrorMessage = "Lütfen Renk Seçimi Yapınız !")]
        public string? Color { get; set; }



        public bool isPublished { get; set; }




        [Required(ErrorMessage = "Lütfen Yayında Kalma Süresini Seçiniz !")]
        public int? Expire { get; set; }




        [Required(ErrorMessage = "Açıklama alanı boş olamaz !")]
        [StringLength(300,MinimumLength =15,ErrorMessage ="Açıklama satırı 15-300 karakterden oluşmaktadır !")]
        public string Description { get; set; }




        [Required(ErrorMessage = "Lütfen Yayımlanma Tarihini Seçiniz !")]
        public DateTime? publishDate { get; set; }

        [ValidateNever]
        public IFormFile? Image { get; set; }

        [ValidateNever]
        public string? ImagePath { get; set; }

        [Required(ErrorMessage = "Lütfen Kategori Seçimi Yapınız !")]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }






        //[Required(ErrorMessage = "Lütfen E-mail adresi giriniz !")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", ErrorMessage = "Geçerli bir mail adres formatı girin !")]
        //public string Email { get; set; }
    }
}
