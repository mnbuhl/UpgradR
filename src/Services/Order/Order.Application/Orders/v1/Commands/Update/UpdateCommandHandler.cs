using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;

namespace Order.Application.Orders.v1.Commands.Update
{
    public class UpdateCommand : IRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        // BillingAddress
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        // Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }

    public class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCommandHandler> _logger;

        public UpdateCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetAsync(x => x.Id == request.Id);

            if (orderToUpdate == null)
            {
                _logger.LogInformation($"Order with id {request.Id} was not found");
                return Unit.Value;
            }

            _mapper.Map(orderToUpdate, request);

            await _orderRepository.UpdateAsync(orderToUpdate);

            _logger.LogInformation($"Order {orderToUpdate.Id} was successfully updated.");

            return Unit.Value;
        }
    }
}