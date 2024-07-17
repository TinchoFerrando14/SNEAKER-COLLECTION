using MediatR;
using SneakerCollectionAPI.Domain.DTOs;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.UpdateSneaker
{
    public class UpdateSneakerCommandHandler : IRequestHandler<UpdateSneakerCommand, SneakerDto>
    {
        private readonly SneakerCollectionContext _context;

        public UpdateSneakerCommandHandler(SneakerCollectionContext context)
        {
            _context = context;
        }

        public async Task<SneakerDto> Handle(UpdateSneakerCommand request, CancellationToken cancellationToken)
        {
            var sneaker = await _context.Sneakers.FindAsync(request.Id);
            if (sneaker == null)
            {
                return null; // Or throw an exception
            }

            sneaker.Name = request.Name;
            sneaker.Brand = request.Brand;
            sneaker.Price = request.Price;
            sneaker.Size = request.Size;
            sneaker.Year = request.Year;
            sneaker.Rate = request.Rate;

            _context.Sneakers.Update(sneaker);
            await _context.SaveChangesAsync(cancellationToken);

            var updatedSneakerDto = new SneakerDto()
            {
                Id = sneaker.Id,
                Name = sneaker.Name,
                Brand = sneaker.Brand,
                Price = sneaker.Price,
                Size = sneaker.Size,
                Year = sneaker.Year,
                Rate = sneaker.Rate,
                UserId = sneaker.UserId,
            };

            return updatedSneakerDto;
        }
    }
}
