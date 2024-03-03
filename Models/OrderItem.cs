using System.Text.Json.Serialization;

namespace ShopSite.CW.WebApp.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; } 
        //public int OrderId { get; set; } // Identifier for the order this item belongs to
        
        public int Quantity { get; set; } // Quantity of the product in this order item

         // Foreign key for Product
        public int ProductId { get; set; }

        // Navigation property for Product
        public Product? Product { get; set; }
    }
}