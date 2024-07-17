using MediatR;
using SneakerCollectionAPI.Domain.DTOs;
using SneakerCollectionAPI.Domain.Entities;

namespace SneakerCollectionAPI.DataAccess.UserDataAccess.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
    }
}
