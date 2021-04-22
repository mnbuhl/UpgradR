using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Queries
{
    public class DeleteQuery : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteQueryHandler : IRequestHandler<DeleteQuery>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeleteQueryHandler> _logger;

        public DeleteQueryHandler(IOrderRepository orderRepository, ILogger<DeleteQueryHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteQuery request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _orderRepository.GetAsync(x => x.Id == request.Id);

            if (orderToDelete == null)
            {
                _logger.LogInformation($"Order with Id: {request.Id} was not found.");
                return Unit.Value;
            }

            await _orderRepository.DeleteAsync(orderToDelete);
            _logger.LogInformation($"Order with Id: {request.Id} has been deleted");

            return Unit.Value;
        }
    }
}