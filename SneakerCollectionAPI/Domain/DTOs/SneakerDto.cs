using SneakerCollectionAPI.Domain.Entities;
using SneakerCollectionAPI.Domain.Enums;

namespace SneakerCollectionAPI.Domain.DTOs
{
    public class SneakerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }

        public int Year { get; set; }

        public RateEnum Rate { get; set; }

        public long UserId {  get; set; }
    }
}
