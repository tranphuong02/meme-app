namespace Transverse.Models.DAL
{
    public partial class Coupon_Category
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int CouponId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Coupon Coupon { get; set; }
    }
}