using System;
using System.Collections.Generic;

namespace Transverse.Models.DAL
{
    public partial class Category
    {
        public Category()
        {
            this.SubCategories = new List<Category>();
            this.CategoryLocalizes = new List<CategoryLocalize>();
            this.Coupon_Category = new List<Coupon_Category>();
            this.Deal_Category = new List<Deal_Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ImageUrl { get; set; }
        public string Color { get; set; }
        public string PinUrl { get; set; }
        public bool IsDeleted { get; set; }
        public int Order { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<CategoryLocalize> CategoryLocalizes { get; set; }
        public virtual ICollection<Coupon_Category> Coupon_Category { get; set; }
        public virtual ICollection<Deal_Category> Deal_Category { get; set; }
    }
}