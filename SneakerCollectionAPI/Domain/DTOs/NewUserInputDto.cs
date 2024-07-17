using System.ComponentModel.DataAnnotations;

namespace SneakerCollectionAPI.Domain.DTOs
{
    public class NewUserInputDto 
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "The password must be a string with at least 10 characters.")]
        public string Password { get; set; }
    }
}
