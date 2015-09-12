using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Constants;

namespace Core.Model.Entities
{
    public class Post : BaseEntity
    {
        [StringLength(AppConstants.LongText)]
        [Required]
        public string Title { get; set; }

        [StringLength(AppConstants.LongText)]
        [Required]
        public string Url { get; set; }

        [StringLength(AppConstants.LongText)]
        [Required]
        public string Keyword { get; set; }

        [StringLength(AppConstants.LongText)]
        [Required]
        public string Description { get; set; }

        public string Sumary { get; set; }

        public string Content { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
