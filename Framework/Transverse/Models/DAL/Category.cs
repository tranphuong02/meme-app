using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transverse.Models.DAL
{
    public class Category : BaseModel
    {
        public string Title { get; set; }
        public string Title1 { get; set; }
        public string Url { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? EndedDate { get; set; }
        public int Status { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        // Helper Properties
        public virtual Author Author { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}