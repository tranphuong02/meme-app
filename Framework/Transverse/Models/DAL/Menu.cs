using System.ComponentModel;

namespace Transverse.Models.DAL
{
    public class Menu : BaseModel
    {
        public string Title { get; set; }
        public string Url { get; set; }

        [DefaultValue(true)]
        public bool IsExternalLink { get; set; }
        public int Index { get; set; }
        public int Order { get; set; }
    }
}