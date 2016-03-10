using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public partial class User
    {
        public User()
        {
            this.CouponsAuthor = new List<Coupon>();
            this.CouponsChecker = new List<Coupon>();
            this.Deals = new List<Deal>();
            this.DealsAuthor = new List<Deal>();
            this.DealsChecker = new List<Deal>();
            this.Histories = new List<History>();
            this.InternalMessagesSender = new List<InternalMessage>();
            this.InternalMessagesReceiver = new List<InternalMessage>();
            this.Notifications = new List<Notification>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime JoinDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public virtual ICollection<Coupon> CouponsAuthor { get; set; }
        public virtual ICollection<Coupon> CouponsChecker { get; set; }
        public virtual ICollection<Deal> Deals { get; set; }
        public virtual ICollection<Deal> DealsAuthor { get; set; }
        public virtual ICollection<Deal> DealsChecker { get; set; }
        public string Token { get; set; }
        public int? Action { get; set; }

        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<InternalMessage> InternalMessagesSender { get; set; }
        public virtual ICollection<InternalMessage> InternalMessagesReceiver { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual Role Role { get; set; }
    }
}