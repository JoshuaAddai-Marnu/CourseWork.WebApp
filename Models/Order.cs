using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShopSite.CW.WebApp.Models
{
    public class Order
    {
        
        public int OrderId { get; set; } 

        public DateTime OrderDate { get; set; } 

        public string CustomerId {get; set;}

        public IdentityUser? Customer {get; set;}

        public string CustomerName { get; set; }

        public string ShippingAddress { get; set; } 

        public decimal? TotalAmount { get; set; } 

        public bool? IsShipped { get; set; } 

        // Navigation property for OrderItems
        public List<OrderItem>? OrderItems { get; set; }

        public Order()
        {
            OrderDate = DateTime.Now;
        }
        
    }
}