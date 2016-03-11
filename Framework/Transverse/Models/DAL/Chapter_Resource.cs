using System.ComponentModel.DataAnnotations.Schema;

namespace Transverse.Models.DAL
{
    public class Chapter_Resource : BaseModel
    {
        [ForeignKey("Chapter")]
        public int ChapterId { get; set; }

        [ForeignKey("Resource")]
        public int ResourceId { get; set; }

        public virtual Chapter Chapter { get; set; }
        public virtual Resource Resource { get; set; }
    }
}