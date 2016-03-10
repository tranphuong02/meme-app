namespace Transverse.Models.DAL
{
    public partial class Deal_Category
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int DealId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Deal Deal { get; set; }
    }
}