using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopSite.CW.WebApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedOn { get; set; }


        // Property for storing product image as binary data
        public byte[]? ImageData { get; set; }
        public string? ImageMimeType { get; set; }

        // Navigation property for Suppliers
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        // Navigation property for Categories
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        public Product()
        {
            CreatedOn = DateTime.Now;
        }
    }
}