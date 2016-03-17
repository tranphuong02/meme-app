using System.Collections.Generic;

namespace Transverse.Models.Business.Genre
{
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public int ViewCount { get; set; }
        public string Resource { get; set; }
        public IEnumerable<string> Categories { get; set; }
    }
}