namespace Transverse.Models.DAL
{
    public class Coupon_Device_Queue
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public int DeviceId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual Device Device { get; set; }
    }
}