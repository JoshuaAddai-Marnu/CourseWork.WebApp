using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopSite.CW.WebApp.Models
{
    public class Order
    {
        
         public int OrderId { get; set; } 
        public string OrderDate { get; set; } 
        public string CustomerName { get; set; } 
        public string CustomerEmail { get; set; } 
        public string ShippingAddress { get; set; } 
        public decimal TotalAmount { get; set; } 
        public bool IsShipped { get; set; } 

        // Navigation property for OrderItems
        [JsonIgnore]
        public List<OrderItem>? OrderItems { get; set; }
        
    }
}