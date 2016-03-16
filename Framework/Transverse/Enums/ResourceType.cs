using System.ComponentModel.DataAnnotations;

namespace Transverse.Enums
{
    public enum ResourceType
    {
        [Display(Name = @"Genre")]
        Genre,

        [Display(Name = @"Author")]
        Author,

        [Display(Name = @"Chapter")]
        Chapter
    }
}