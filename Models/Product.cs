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
        public string CreatedOn { get; set; } 
        

         // Property for storing product image as binary data
         [JsonIgnore]
        public byte[]? ImageData { get; set; }
        [JsonIgnore]
        public string? ImageMimeType { get; set; }
         
         // Navigation property for Suppliers
        public List<Supplier>? Suppliers { get; set; }
        
        // Navigation property for Categories
        public List<Category>? Categories { get; set; }
    }
}