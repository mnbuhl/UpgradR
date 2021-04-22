using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Order.Application.Interfaces;

namespace Order.Application.Orders.v1.Queries.Get
{
    public class GetQuery : IRequest<List<OrderResponseDto>>
    {
        public string Username { get; set; }
    }

    public class GetQueryHandler : IRequestHandler<GetQuery, List<OrderResponseDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponseDto>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUsername(request.Username);
            return _mapper.Map<List<OrderResponseDto>>(orders);
        }
    }
}