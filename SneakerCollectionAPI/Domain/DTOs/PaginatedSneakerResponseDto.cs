namespace SneakerCollectionAPI.Domain.DTOs
{
    public class PaginatedSneakerResponse
    {
        public IEnumerable<SneakerDto> Sneakers { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
