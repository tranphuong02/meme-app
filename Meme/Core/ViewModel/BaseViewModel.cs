using System;
using System.ComponentModel.DataAnnotations;

namespace Core.ViewModel
{
    public class BaseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Is Delete")]
        public bool IsDelete { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
