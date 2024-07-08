﻿namespace MyAspNetCoreApp.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? Color { get; set; }
        public bool isPublished { get; set; }

        public int Expire {  get; set; }
        public string Description { get; set; }
        public  DateTime? publishDate { get; set; }
        public string? ImagePath { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        

    }
}
