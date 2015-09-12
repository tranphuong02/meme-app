using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Constants;

namespace Core.Model.Entities
{
    public class Category : BaseEntity
    {
        [StringLength(AppConstants.LongText)]
        [Required]
        public string Title { get; set; }

        [StringLength(AppConstants.LongText)]
        [Required]
        public string Url { get; set; }

        [StringLength(AppConstants.LongText)]
        public string TitleVI { get; set; }

        [StringLength(AppConstants.NormalText)]
        public string Type { get; set; }

        [StringLength(AppConstants.NormalText)]
        public string Author { get; set; }

        [StringLength(AppConstants.NormalText)]
        public string Status { get; set; }

        [StringLength(AppConstants.LongText)]
        [Required]
        public string Keyword { get; set; }

        [StringLength(AppConstants.LongText)]
        [Required]
        public string Description { get; set; }

        [StringLength(AppConstants.LongText)]
        public string Image { get; set; }

        public string Sumary { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
