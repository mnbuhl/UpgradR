using System.ComponentModel.DataAnnotations;

namespace Discount.Core.Contracts.v1.Coupons
{
    public class CreateCouponDto
    {
        [Required]
        [MaxLength(24), MinLength(24)]
        public string ProductId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}