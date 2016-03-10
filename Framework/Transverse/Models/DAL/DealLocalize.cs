namespace Transverse.Models.DAL
{
    public partial class DealLocalize
    {
        public int Id { get; set; }
        public int DealId { get; set; }
        public string LangName { get; set; }
        public string CardImage { get; set; }
        public string CardType { get; set; }
        public string Keywords { get; set; }
        public string TCUrl { get; set; }
        public string PromoConditions { get; set; }
        public string PromoDescription { get; set; }
        public virtual Deal Deal { get; set; }
    }
}