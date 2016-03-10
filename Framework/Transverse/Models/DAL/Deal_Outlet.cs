namespace Transverse.Models.DAL
{
    public partial class Deal_Outlet
    {
        public int Id { get; set; }
        public int DealId { get; set; }
        public int OutletId { get; set; }
        public virtual Deal Deal { get; set; }
        public virtual Outlet Outlet { get; set; }
    }
}