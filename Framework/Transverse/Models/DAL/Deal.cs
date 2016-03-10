using System;
using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public partial class Deal
    {
        public Deal()
        {
            this.Deal_Category = new List<Deal_Category>();
            this.Deal_Outlet = new List<Deal_Outlet>();
            this.DealLocalizes = new List<DealLocalize>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PromoImageUrl { get; set; }
        public string PromoThumbnailUrl { get; set; }
        public System.DateTime ValidTill { get; set; }
        public Nullable<System.DateTime> NewValidTill { get; set; }
        public int ViewCount { get; set; }
        public int ProcessCode { get; set; }
        public bool IsOnlineMerchant { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public int AuthorId { get; set; }
        public int? CheckerId { get; set; }
        public bool IsChooseAllOutlet { get; set; }
        public virtual ICollection<Deal_Category> Deal_Category { get; set; }
        public virtual ICollection<Deal_Outlet> Deal_Outlet { get; set; }
        public virtual ICollection<DealLocalize> DealLocalizes { get; set; }
        public virtual User Author { get; set; }
        public virtual User Checker { get; set; }
    }
}