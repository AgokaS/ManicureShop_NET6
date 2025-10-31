using System.ComponentModel.DataAnnotations;

namespace ManicureShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required, Range(0, 100000)]
        public decimal Price { get; set; }

        public string? ImagePath { get; set; }
    }
}
