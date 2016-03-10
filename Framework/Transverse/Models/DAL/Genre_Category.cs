using System.ComponentModel.DataAnnotations.Schema;

namespace Transverse.Models.DAL
{
    public class Genre_Category : BaseModel
    {
        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Helper Properties
        public virtual Genre Genre { get; set; }

        public virtual Category Category { get; set; }
    }
}