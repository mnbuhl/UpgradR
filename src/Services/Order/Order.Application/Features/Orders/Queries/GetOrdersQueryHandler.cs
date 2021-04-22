using AutoMapper;
using MediatR;
using Order.Application.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.Orders.Queries
{
    public class GetOrdersQuery : IRequest<List<OrderResponseDto>>
    {
        public string Username { get; set; }
    }

    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, List<OrderResponseDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponseDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUsername(request.Username);
            return _mapper.Map<List<OrderResponseDto>>(orders);
        }
    }
}