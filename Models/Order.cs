using System.ComponentModel.DataAnnotations;

namespace ManicureShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        public DateTime Date { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public ICollection<OrderItem>? Items { get; set; }
    }

    public class OrderItem
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Product? Product { get; set; }
        public Order? Order { get; set; }
    }
}
