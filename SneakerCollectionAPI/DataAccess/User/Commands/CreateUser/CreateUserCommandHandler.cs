using MediatR;
using SneakerCollectionAPI.Domain.Entities;
using SneakerCollectionAPI.Helpers;

namespace SneakerCollectionAPI.DataAccess.UserDataAccess.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly SneakerCollectionContext _context;

        public CreateUserCommandHandler(SneakerCollectionContext userRepository)
        {
            _context = userRepository;
        }

        public async Task<int> Handle(CreateUserCommand user,
        CancellationToken cancellationToken = default)
        {
            var hashedPassword = PasswordHasher.HashPassword(user.Password);

            var newUser = new User
            {
                Email = user.Email,
                Password = hashedPassword
            };

            await _context.Users.AddAsync(newUser);
            return await _context.SaveChangesAsync();
        }
    }
}
