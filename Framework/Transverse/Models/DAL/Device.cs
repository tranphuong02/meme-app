using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public partial class Device
    {
        public Device()
        {
            this.Coupon_RedeemCode_Device = new List<Coupon_RedeemCode_Device>();
        }

        public int Id { get; set; }
        public string DeviceId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual ICollection<Coupon_RedeemCode_Device> Coupon_RedeemCode_Device { get; set; }
        public virtual ICollection<Coupon_Device_Queue> Coupon_Device_Queue { get; set; }
    }
}