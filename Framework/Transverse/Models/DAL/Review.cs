using System.ComponentModel.DataAnnotations.Schema;

namespace Transverse.Models.DAL
{
    public class Review : BaseModel
    {
        public double Score { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("User")]
        public int? UserId { get; set; }

        // Helper Properties
        public virtual Category Category { get; set; }

        public virtual User User { get; set; }
    }
}