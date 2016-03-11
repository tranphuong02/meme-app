using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public class Resource : BaseModel
    {
        public string Url { get; set; }
        public string Tag { get; set; }
        public int Order { get; set; }
        public int Type { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Chapter_Resource> ChapterResources { get; set; }
    }
}