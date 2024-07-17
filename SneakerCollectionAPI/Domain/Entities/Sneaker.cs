using SneakerCollectionAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SneakerCollectionAPI.Domain.Entities
{
    public class Sneaker
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public RateEnum Rate { get; set; }

        // Foreign key
        [Required]
        public long UserId { get; set; }

        // Navigation property
        [Required]
        public User User { get; set; }
    }
}
