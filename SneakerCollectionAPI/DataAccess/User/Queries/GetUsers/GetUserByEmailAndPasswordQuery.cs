using MediatR;
using SneakerCollectionAPI.Domain.Entities;

namespace SneakerCollectionAPI.DataAccess.UserDataAccess.Queries.GetUsers
{
    public class GetUserByEmailAndPasswordQuery(string email, string password) : IRequest<User>
    {
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
    }
}
