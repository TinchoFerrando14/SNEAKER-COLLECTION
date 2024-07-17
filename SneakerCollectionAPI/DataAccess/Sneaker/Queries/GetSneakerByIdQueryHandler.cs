using MediatR;
using Microsoft.EntityFrameworkCore;
using SneakerCollectionAPI.Domain.DTOs;
using SneakerCollectionAPI.Domain.Entities;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Queries
{   
    public class GetSneakerByIdQueryHandler : IRequestHandler<GetSneakerByIdQuery, SneakerDto>
    {
        private readonly SneakerCollectionContext _context;

        public GetSneakerByIdQueryHandler(SneakerCollectionContext context)
        {
            _context = context;
        }

        public async Task<SneakerDto> Handle(GetSneakerByIdQuery request, CancellationToken cancellationToken)
        {
            // Fetch sneaker from the database
            var sneaker = await _context.Sneakers
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

            if (sneaker == null){
                return null;
            }

            var sneakerDto = new SneakerDto
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

            return sneakerDto;
        }

    }
}
