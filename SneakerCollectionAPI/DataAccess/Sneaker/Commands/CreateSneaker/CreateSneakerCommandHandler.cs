using MediatR;
using SneakerCollectionAPI.Domain.Entities;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.CreateSneaker
{
    public class CreateSneakerCommandHandler : IRequestHandler<CreateSneakerCommand, Sneaker>
    {
        private readonly SneakerCollectionContext _context;

        public CreateSneakerCommandHandler(SneakerCollectionContext context)
        {
            _context = context;
        }

        public async Task<Sneaker> Handle(CreateSneakerCommand sneaker, CancellationToken cancellationToken)
        {
            var sneakerToCreate = new Sneaker
            {
                Name = sneaker.Name,
                Brand = sneaker.Brand,
                Price = sneaker.Price,                
                Size = sneaker.Size,
                Year = sneaker.Year,
                Rate = sneaker.Rate,
                UserId = sneaker.UserId,
            };

            _context.Sneakers.Add(sneakerToCreate);
            await _context.SaveChangesAsync(cancellationToken);

            return sneakerToCreate;
        }
    }
}
