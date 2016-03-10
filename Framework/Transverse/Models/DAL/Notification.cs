using System;

namespace Transverse.Models.DAL
{
    public partial class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public int Type { get; set; }

        public int Data { get; set; }

        public bool IsAuto { get; set; }
        public bool IsSend { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> TimeSend { get; set; }
        public int AuthorId { get; set; }
        public virtual User User { get; set; }
    }
}