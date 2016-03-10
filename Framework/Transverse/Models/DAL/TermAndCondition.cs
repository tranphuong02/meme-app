//////////////////////////////////////////////////////////////////////
// File Name    : TermAndCondition
// System Name  : BreezeGoodlife
// Summary      :
// Author       : phuong.tran
// Change Log   : 12/30/2015 3:57:26 PM - Create Date
/////////////////////////////////////////////////////////////////////

namespace Transverse.Models.DAL
{
    public class TermAndCondition
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
    }
}