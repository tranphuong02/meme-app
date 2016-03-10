using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public class Role : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}