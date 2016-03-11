using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transverse.Models.DAL
{
    public class Author : BaseModel
    {
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Country { get; set; }

        [ForeignKey("Resource")]
        public int ResourceId { get; set; }

        // Helper Properties
        public virtual ICollection<Category> Categories { get; set; }

        public virtual Resource Resource { get; set; }
    }
}