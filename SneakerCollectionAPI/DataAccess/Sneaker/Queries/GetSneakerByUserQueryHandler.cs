using MediatR;
using Microsoft.EntityFrameworkCore;
using SneakerCollectionAPI.DataAccess.SneakerDataAccess.Queries;
using SneakerCollectionAPI.Domain.DTOs;
using SneakerCollectionAPI.Domain.Entities;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Queries
{  
    public class GetSneakerByUserQueryHandler : IRequestHandler<GetSneakerByUserQuery, PaginatedSneakerResponse>
    {
        private readonly SneakerCollectionContext _context;

        public GetSneakerByUserQueryHandler(SneakerCollectionContext context)
        {
            _context = context;
        }

        public async Task<PaginatedSneakerResponse> Handle(GetSneakerByUserQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _context.Sneakers.CountAsync(s => s.UserId == request.UserId, cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var sneakers = await _context.Sneakers
                .Where(s => s.UserId == request.UserId)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var sneakerDtos = sneakers.Select(sneaker => new SneakerDto
            {
                Id = sneaker.Id,
                Name = sneaker.Name,
                Brand = sneaker.Brand,
                Price = sneaker.Price,
                Rate = sneaker.Rate,
                Size = sneaker.Size,
                Year = sneaker.Year,
            });

            return new PaginatedSneakerResponse
            {
                Sneakers = sneakerDtos,
                TotalPages = totalPages,
                TotalCount = totalCount
            };
        }

    }
}
