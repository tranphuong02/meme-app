using System;

namespace Transverse.Models.DAL
{
    public partial class History
    {
        public int Id { get; set; }
        public Nullable<int> ParentId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public int ActionType { get; set; }
        public string TableName { get; set; }
        public Nullable<int> RowId { get; set; }
        public string PreviousData { get; set; }
        public System.DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
    }
}