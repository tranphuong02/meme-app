using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public partial class Merchant
    {
        public Merchant()
        {
            this.Outlets = new List<Outlet>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string WebsiteUrl { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public virtual ICollection<Outlet> Outlets { get; set; }
    }
}