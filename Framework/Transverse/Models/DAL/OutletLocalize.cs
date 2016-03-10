namespace Transverse.Models.DAL
{
    public partial class OutletLocalize
    {
        public int Id { get; set; }
        public int OutletId { get; set; }
        public string Address { get; set; }
        public string LangName { get; set; }
        public virtual Outlet Outlet { get; set; }
    }
}