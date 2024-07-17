using MediatR;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.DeleteSneaker
{
    public class DeleteSneakerCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }
}
