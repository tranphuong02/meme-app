namespace Transverse.Models.DAL
{
    public partial class Coupon_Outlet
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int OutletId { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual Outlet Outlet { get; set; }
    }
}