using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopSite.CW.WebApp.Models
{
    public class Category
    {
        
        public int CategoryId { get; set; } 
        public string Name { get; set; } 
        public string? Description { get; set; } 

          // Navigation property for Products
          [JsonIgnore]
        public List<Product>? Products { get; set; }
    }
}