using System;
using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public partial class Coupon
    {
        public Coupon()
        {
            this.Coupon_Category = new List<Coupon_Category>();
            this.Coupon_RedeemCode_Device = new List<Coupon_RedeemCode_Device>();
            this.Coupon_Outlet = new List<Coupon_Outlet>();
            this.CouponLocalizes = new List<CouponLocalize>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string PromoImageUrl { get; set; }
        public string PromoThumbnailUrl { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime ValidTill { get; set; }
        public Nullable<System.DateTime> NewValidTill { get; set; }
        public int LimitQuantity { get; set; }
        public int ViewCount { get; set; }
        public int ProcessCode { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public int AuthorId { get; set; }
        public Nullable<int> CheckerId { get; set; }
        public bool IsChooseAllOutlet { get; set; }
        public virtual User Author { get; set; }
        public virtual User Checker { get; set; }
        public virtual ICollection<Coupon_Category> Coupon_Category { get; set; }
        public virtual ICollection<Coupon_Device_Queue> Coupon_Device_Queue { get; set; }
        public virtual ICollection<Coupon_RedeemCode_Device> Coupon_RedeemCode_Device { get; set; }
        public virtual ICollection<Coupon_Outlet> Coupon_Outlet { get; set; }
        public virtual ICollection<CouponLocalize> CouponLocalizes { get; set; }
    }
}