namespace Transverse.Utils
{
    public static class LayoutHelpers
    {
        public static string IsSelected(int id, int compareId)
        {
            return id == compareId ? "selected" : "";
        }
    }
}