using AutoMapper;
using Basket.Core.Contracts.v1.CheckoutBaskets;
using Basket.Core.Contracts.v1.ShoppingCarts;
using Basket.Core.Entities;
using Basket.Core.Interfaces;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Basket.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketController> _logger;
        private readonly IDiscountGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger,
            IMapper mapper, IDiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _mapper = mapper;
            _discountGrpcService = discountGrpcService;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponseDto>> Get(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);

            if (basket == null)
            {
                _logger.LogInformation($"Basket for user: {userName} not found. Creating new.");
                return Ok(new ShoppingCartResponseDto { UserName = userName });
            }

            var basketResponse = _mapper.Map<ShoppingCartResponseDto>(basket);

            return Ok(basketResponse);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            // Get shopping cart
            var basket = await _basketRepository.GetBasket(basketCheckout.UserName);
            var basketDto = _mapper.Map<ShoppingCartResponseDto>(basket);

            if (basket == null)
                return NotFound();

            // Create eventMessage and send it to RabbitMQ with MassTransit
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basketDto.TotalPrice;

            await _publishEndpoint.Publish(eventMessage);

            // Remove the users basket. 
            await _basketRepository.DeleteBasket(basketDto.UserName);

            return Accepted();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponseDto>> Update([FromBody] ShoppingCartCommandDto basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductId);
                item.Price -= coupon.Amount;
            }

            var updatedBasket = await _basketRepository.UpdateBasket(_mapper.Map<ShoppingCart>(basket));

            return Ok(_mapper.Map<ShoppingCartResponseDto>(updatedBasket));
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return NoContent();
        }
    }
}
