namespace Transverse.Models.DAL
{
    public class Config : BaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Type { get; set; }
    }
}