using MediatR;
using SneakerCollectionAPI.Domain.DTOs;
using SneakerCollectionAPI.Domain.Enums;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.UpdateSneaker
{
    public class UpdateSneakerCommand : IRequest<SneakerDto>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }
        public int Year { get; set; }
        public RateEnum Rate { get; set; }
    }
}
