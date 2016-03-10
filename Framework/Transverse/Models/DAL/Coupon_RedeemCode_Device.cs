namespace Transverse.Models.DAL
{
    public partial class Coupon_RedeemCode_Device
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int? DeviceId { get; set; }
        public string RedeemCode { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual Device Device { get; set; }
    }
}