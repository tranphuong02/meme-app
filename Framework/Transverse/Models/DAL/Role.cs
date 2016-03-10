using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public partial class Role
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}