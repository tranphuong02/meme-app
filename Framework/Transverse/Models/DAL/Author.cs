using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public class Author : BaseModel
    {
        public string Name { get; set; }
        public string Country { get; set; }

        // Helper Properties
        public virtual ICollection<Category> Categories { get; set; }
    }
}