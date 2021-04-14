using System.ComponentModel.DataAnnotations;

namespace Discount.Core.Contracts.v1.Coupons
{
    public class UpdateCouponDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(24), MinLength(24)]
        public string ProductId { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }
    }
}