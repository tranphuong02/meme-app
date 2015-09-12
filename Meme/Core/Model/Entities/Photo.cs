using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Constants;

namespace Core.Model.Entities
{
    public class Photo : BaseEntity
    {
        [StringLength(AppConstants.NormalText)]
        public string Title { get; set; }

        [StringLength(AppConstants.NormalText)]
        public string Alt { get; set; }

        [StringLength(AppConstants.LongText)]
        public string Path { get; set; }

        public int PostId { get; set; }

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
    }
}
