
using System.Linq;

namespace Core.Utils
{
    public class UrlRewrite
    {
        private static readonly string[] a = new[]
                                             {
                                                 "à", "á", "ạ", "ả", "ã", "â", "ầ", "ấ", "ậ", "ẩ", "ẫ", "ă", "ắ", "ằ", "ắ",
                                                 "ặ", "ẳ", "ẵ", "a"
                                             };

        private static readonly string[] d = new[] { "đ", "d" };
        private static readonly string[] e = new[] { "è", "é", "ẹ", "ẻ", "ẽ", "ê", "ề", "ế", "ệ", "ể", "ễ", "e" };
        private static readonly string[] ii = new[] { "ì", "í", "ị", "ỉ", "ĩ", "i" };
        private static readonly string[] y = new[] { "ỳ", "ý", "ỵ", "ỷ", "ỹ", "y" };

        private static readonly string[] o = new[]
                                             {
                                                 "ò", "ó", "ọ", "ỏ", "õ", "ô", "ồ", "ố", "ộ", "ổ", "ỗ", "ơ", "ờ", "ớ", "ợ",
                                                 "ở", "ỡ", "o"
                                             };

        private static readonly string[] u = new[] { "ù", "ú", "ụ", "ủ", "ũ", "ừ", "ứ", "ự", "ử", "ữ", "u", "ư" };

        public static string GenAlilas(string longName)
        {
            var ret = string.Empty;
            var len = longName.Length;
            if (longName.Length > 0)
            {
                int i;
                for (i = 0; i < len; i++)
                {
                    var currentchar = longName.Substring(i, 1);
                    ret = ret + ChangeChar(currentchar);
                }
            }
            else
                ret = "";
            return ret;
        }

        public static string GenShortName(string longName)
        {
            var ret = string.Empty;
            longName = longName.Replace(" ", "-");
            longName = longName.Replace(")", "");
            longName = longName.Replace("(", "");
            longName = longName.Replace("*", "");
            longName = longName.Replace("[", "");
            longName = longName.Replace("]", "");
            longName = longName.Replace("}", "");
            longName = longName.Replace("{", "");
            longName = longName.Replace(">", "");
            longName = longName.Replace("<", "");
            longName = longName.Replace("=", "");
            longName = longName.Replace(":", "");
            longName = longName.Replace(",", "");
            longName = longName.Replace("'", "");
            longName = longName.Replace("\"", "");
            longName = longName.Replace("/", "");
            longName = longName.Replace("\\", "");
            longName = longName.Replace("#", "");
            longName = longName.Replace("&", "");
            longName = longName.Replace("?", "");
            longName = longName.Replace(";", "");
            longName = longName.ToLower();
            var len = longName.Length;
            if (longName.Length > 0)
            {
                int i;
                for (i = 0; i < len; i++)
                {
                    var currentchar = longName.Substring(i, 1);
                    ret = ret + ChangeChar(currentchar);
                }
            }
            else
                ret = "";
            return ret;
        }

        private static string ChangeChar(string charinput)
        {
            if (a.Any(t => t.Equals(charinput)))
                return "a";
            if (d.Any(t => t.Equals(charinput)))
                return "d";
            if (e.Any(t => t.Equals(charinput)))
                return "e";
            if (ii.Any(t => t.Equals(charinput)))
                return "i";
            if (y.Any(t => t.Equals(charinput)))
                return "y";
            if (o.Any(t => t.Equals(charinput)))
                return "o";
            return u.Any(t => t.Equals(charinput)) ? "u" : charinput;
        }

        public static string GenUserName(string longName)
        {
            if (string.IsNullOrWhiteSpace(longName))
                return "";
            var ret = string.Empty;
            longName = longName.Replace(" ", ".");
            longName = longName.Replace(")", "");
            longName = longName.Replace("(", "");
            longName = longName.Replace("*", "");
            longName = longName.Replace("[", "");
            longName = longName.Replace("]", "");
            longName = longName.Replace("}", "");
            longName = longName.Replace("{", "");
            longName = longName.Replace(">", "");
            longName = longName.Replace("<", "");
            longName = longName.Replace("=", "");
            longName = longName.Replace(":", "");
            longName = longName.Replace(",", "");
            longName = longName.Replace("'", "");
            longName = longName.Replace("\"", "");
            longName = longName.Replace("/", "");
            longName = longName.Replace("\\", "");
            longName = longName.Replace("#", "");
            longName = longName.Replace("&", "");
            longName = longName.Replace("?", "");
            longName = longName.Replace(";", "");
            longName = longName.ToLower();
            var len = longName.Length;
            if (longName.Length > 0)
            {
                int i;
                for (i = 0; i < len; i++)
                {
                    var currentchar = longName.Substring(i, 1);
                    ret = ret + ChangeChar(currentchar);
                }
            }
            else
                ret = "";
            return ret;
        }
    }
}
