using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;
using Order.Application.Models;
using Order.Application.Orders.v1.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Orders.v1.Commands.Checkout
{
    public class CheckoutCommand : IRequest<OrderResponseDto>
    {
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

    public class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, OrderResponseDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutCommandHandler> _logger;

        public CheckoutCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<OrderResponseDto> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Domain.Entities.Order>(request);
            var createdOrder = await _orderRepository.AddAsync(order);

            _logger.LogInformation($"Order with Id: {createdOrder.Id} has been created");

            await SendMail(createdOrder);

            return _mapper.Map<OrderResponseDto>(createdOrder);
        }

        private async Task SendMail(Domain.Entities.Order order)
        {
            var email = new Email
            {
                To = order.EmailAddress,
                Subject = $"Order {order.Id} confirmation",
                Body = "Order was created"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Failed to send confirmation email for order: {order.Id}");
            }
        }
    }
}