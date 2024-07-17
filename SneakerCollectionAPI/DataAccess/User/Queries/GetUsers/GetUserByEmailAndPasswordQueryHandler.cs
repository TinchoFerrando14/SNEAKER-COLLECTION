using MediatR;
using Microsoft.EntityFrameworkCore;
using SneakerCollectionAPI.Domain.Entities;

namespace SneakerCollectionAPI.DataAccess.UserDataAccess.Queries.GetUsers
{
    public class GetUserByEmailAndPasswordQueryHandler : IRequestHandler<GetUserByEmailAndPasswordQuery, User>
    {
        private readonly SneakerCollectionContext _context;

        public GetUserByEmailAndPasswordQueryHandler(SneakerCollectionContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(GetUserByEmailAndPasswordQuery request, CancellationToken cancellationToken)
        {
            // Fetch user from the database
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            // Here, you should validate the password (hashing and comparison)
            if (user != null && request.Password == user.Password) // Implement your password verification logic
            {
                return user;
            }

            return null; // Return null if user not found or password is invalid
        }

    }
}
