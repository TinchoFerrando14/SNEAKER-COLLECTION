using MediatR;

namespace SneakerCollectionAPI.DataAccess.UserDataAccess.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; internal set; }
    }
}
