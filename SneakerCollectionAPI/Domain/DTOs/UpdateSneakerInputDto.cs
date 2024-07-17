using SneakerCollectionAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SneakerCollectionAPI.Domain.DTOs
{
    public class UpdateSneakerInputDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required]
        [Range(5, 50, ErrorMessage = "Size must be between 5 and 50.")]
        public int Size { get; set; }

        [Required]
        [Range(1950, 2100, ErrorMessage = "Year must be between 1950 and 2100.")]
        public int Year { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public RateEnum Rate { get; set; }
    }
}
