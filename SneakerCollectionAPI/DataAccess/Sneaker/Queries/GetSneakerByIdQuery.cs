using MediatR;
using SneakerCollectionAPI.Domain.DTOs;
using SneakerCollectionAPI.Domain.Entities;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Queries
{
    public class GetSneakerByIdQuery(long id) : IRequest<SneakerDto>
    {
        public long Id { get; set; } = id;
    }
}
