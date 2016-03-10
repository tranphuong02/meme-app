using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public partial class Outlet
    {
        public Outlet()
        {
            this.Coupon_Outlet = new List<Coupon_Outlet>();
            this.Deal_Outlet = new List<Deal_Outlet>();
            this.OutletLocalizes = new List<OutletLocalize>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MerchantId { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public virtual ICollection<Coupon_Outlet> Coupon_Outlet { get; set; }
        public virtual ICollection<Deal_Outlet> Deal_Outlet { get; set; }
        public virtual Merchant Merchant { get; set; }
        public virtual ICollection<OutletLocalize> OutletLocalizes { get; set; }
    }
}