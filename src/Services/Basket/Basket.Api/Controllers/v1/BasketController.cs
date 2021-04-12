using AutoMapper;
using Basket.Core.Contracts.v1.ShoppingCarts;
using Basket.Core.Entities;
using Basket.Core.Interfaces;
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

        public BasketController(IBasketRepository basketRepository, ILogger<BasketController> logger,
            IMapper mapper)
        {
            _basketRepository = basketRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponseDto>> Get(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);

            if (basket == null)
                return Ok(new ShoppingCartResponseDto { UserName = userName });

            var basketResponse = _mapper.Map<ShoppingCartResponseDto>(basket);

            return Ok(basketResponse);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartResponseDto>> Update([FromBody] ShoppingCartCommandDto basket)
        {
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
