using SneakerCollectionAPI.Domain.Entities;
using SneakerCollectionAPI.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SneakerCollectionAPI.Domain.DTOs
{
    public class UserDto
    {
        public long Id { get; set; }

        [Required]
        public string Email { get; set; }

   
        public RoleEnum Role { get; set; }

        
        public ICollection<Sneaker> Sneakers { get; set; } = new List<Sneaker>();
    }
}
