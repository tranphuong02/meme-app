namespace Transverse.Models.DAL
{
    public partial class InternalMessage
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}