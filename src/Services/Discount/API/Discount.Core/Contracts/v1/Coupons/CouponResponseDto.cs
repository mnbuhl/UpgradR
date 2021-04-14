namespace Discount.Core.Contracts.v1.Coupons
{
    public class CouponResponseDto
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}