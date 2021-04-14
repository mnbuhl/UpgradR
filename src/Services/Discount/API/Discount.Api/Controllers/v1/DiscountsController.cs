using AutoMapper;
using Discount.Core.Contracts.v1.Coupons;
using Discount.Core.Entities;
using Discount.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountsController(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<CouponResponseDto>>> GetAll()
        {
            var coupons = await _discountRepository.GetAllDiscounts();
            var couponsResponse = _mapper.Map<List<CouponResponseDto>>(coupons);
            return Ok(couponsResponse);
        }

        [HttpGet("{productId}", Name = "GetDiscount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CouponResponseDto>> Get(string productId)
        {
            var coupon = await _discountRepository.GetDiscount(productId);

            if (coupon == null)
                return NotFound();

            return Ok(_mapper.Map<CouponResponseDto>(coupon));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CouponResponseDto>> Create([FromBody] CreateCouponDto couponRequest)
        {
            var coupon = _mapper.Map<Coupon>(couponRequest);

            bool success = await _discountRepository.CreateDiscount(coupon);

            if (!success)
                return BadRequest();

            var couponResponse = _mapper.Map<CouponResponseDto>(coupon);

            return CreatedAtRoute("GetDiscount", new { productId = couponResponse.ProductId }, couponResponse);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update([FromBody] UpdateCouponDto couponRequest)
        {
            var coupon = _mapper.Map<Coupon>(couponRequest);

            bool success = await _discountRepository.UpdateDiscount(coupon);

            if (!success)
                return BadRequest();

            return NoContent();
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(string productId)
        {
            bool success = await _discountRepository.DeleteDiscount(productId);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
