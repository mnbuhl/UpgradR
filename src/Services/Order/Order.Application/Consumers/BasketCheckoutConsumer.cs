using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Orders.v1.Commands.Checkout;
using System.Threading.Tasks;

namespace Order.Application.Consumers
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMapper mapper, ILogger<BasketCheckoutConsumer> logger, IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation($"BasketCheckoutEvent consumed successfully. Created Order Id: {result.Id}");
        }
    }
}