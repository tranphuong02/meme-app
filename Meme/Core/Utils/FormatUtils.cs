namespace Core.Utils
{
    public class FormatUtils
    {
        public static string FormatCurrency(dynamic currency)
        {
            return "$ " + currency;
        }

        public static string FormatPercen(dynamic percen)
        {
            return percen + " %";
        }

        public static string FormatDateTime(dynamic datetime)
        {
            return datetime.ToString("dd/MM/yyyy");
        }

        public static string ToLower(string obj)
        {
            return obj.ToLower();
        }
    }
}
