using MediatR;

namespace SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.DeleteSneaker
{
    public class DeleteSneakerCommandHandler : IRequestHandler<DeleteSneakerCommand, bool>
    {
        private readonly SneakerCollectionContext _context;

        public DeleteSneakerCommandHandler(SneakerCollectionContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteSneakerCommand request, CancellationToken cancellationToken)
        {
            var sneaker = await _context.Sneakers.FindAsync(request.Id);

            if (sneaker == null)
            {
                return false;
            }

            _context.Sneakers.Remove(sneaker);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
