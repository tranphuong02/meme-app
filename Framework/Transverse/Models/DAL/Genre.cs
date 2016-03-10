using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transverse.Models.DAL
{
    public class Genre : BaseModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        [ForeignKey("Resource")]
        public int ResourceId { get; set; }

        // Helper Properties
        public virtual Resource Resource { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}