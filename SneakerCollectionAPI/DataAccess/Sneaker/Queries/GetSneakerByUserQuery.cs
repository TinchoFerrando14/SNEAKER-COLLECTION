using MediatR;
using SneakerCollectionAPI.Domain.DTOs;
using SneakerCollectionAPI.Domain.Entities;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Queries
{
    public class GetSneakerByUserQuery(long userId, int pageNumber,int pageSize) : IRequest<PaginatedSneakerResponse>
    {
        public long UserId { get; set; } = userId;
        public int PageNumber { get; set; } = pageNumber;
        public int PageSize { get; set; } = pageSize;
    }
}
