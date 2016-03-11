using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public class Chapter : BaseModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }

        // Helper Properties
        public virtual ICollection<Chapter_Resource> ChapterResources { get; set; }
    }
}