using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transverse.Models.DAL
{
    public class Chapter : BaseModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }


        // Helper Properties
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Chapter_Resource> ChapterResources { get; set; }
    }
}