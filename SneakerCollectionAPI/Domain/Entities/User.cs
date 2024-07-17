using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SneakerCollectionAPI.Domain.Enums;

namespace SneakerCollectionAPI.Domain.Entities
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public RoleEnum Role { get; set; }

        // Navigation property
        public ICollection<Sneaker> Sneakers { get; set; } = new List<Sneaker>();

    }
}
