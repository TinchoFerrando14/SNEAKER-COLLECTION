using MediatR;
using SneakerCollectionAPI.Domain.Entities;
using SneakerCollectionAPI.Domain.Enums;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.CreateSneaker
{
    public class CreateSneakerCommand : IRequest<Sneaker>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Size { get; set; }

        public int Year { get; set; }

        public RateEnum Rate { get; set; }
        public long UserId { get; internal set; }
    }
}
