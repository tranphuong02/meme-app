using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Core.Utils
{
    public static class StringUtils
    {
        public static string GetRandomString(int len)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            var result = new StringBuilder(5);

            crypto.GetNonZeroBytes(data);
            data = new byte[len];
            crypto.GetNonZeroBytes(data);
            
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }

        public static string GetValue(string configValue, string defaultValue)
        {
            return string.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }

        public static string ToCountableNouns(string unit, double value)
        {
            if (value <= 1) return string.Format("{0} {1}", value, unit);

            if (unit.EndsWith("s") || unit.EndsWith("ch") || unit.EndsWith("x") || unit.EndsWith("z") || unit.EndsWith("sh"))
                return string.Format("{0} {1}", value, unit + "es");
            
            return string.Format("{0} {1}", value, unit + "s");
        }

        public static string AddSpacesToSentence(string text, bool preserveAcronyms = true)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            var newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        public static string UppercaseFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static string Decode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            return HttpUtility.HtmlDecode(text);
        }

       
    }
}
