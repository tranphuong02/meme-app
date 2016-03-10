namespace Transverse.Models.DAL
{
    public partial class CategoryLocalize
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LangName { get; set; }
        public virtual Category Category { get; set; }
    }
}