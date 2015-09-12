using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Common.Constants;
using Common.Enums;

namespace Core.Utils
{
    public static class Utils
    {
        #region Base Utils

        public static string GetConfigValue(string configValue, string defaultValue)
        {
            return string.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }

        public static int GetConfigValue(string configValue, int defaultValue)
        {
            return string.IsNullOrEmpty(configValue) ? defaultValue : Convert.ToInt32(configValue);
        }

        #endregion

        #region Image Utils

        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.Bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.Gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.Png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.Tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.Tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.Jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.Jpeg;

            return null;
        }

        public static ImageFormat GetImageFormat(string fileName)
        {
            fileName = fileName.ToLower();
            if (fileName.EndsWith(".jpeg") || fileName.EndsWith(".jpg"))
                return ImageFormat.Jpeg;

            if (fileName.EndsWith(".bmp"))
                return ImageFormat.Bmp;

            if (fileName.EndsWith(".gif"))
                return ImageFormat.Gif;

            if (fileName.EndsWith(".png"))
                return ImageFormat.Png;

            if (fileName.EndsWith(".tiff"))
                return ImageFormat.Tiff;

            return null;
        }

        public static bool IsImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            fileName = fileName.ToLower();
            foreach (var extension in EnumUtils.ImageExtensions)
            {
                if (fileName.EndsWith(extension))
                    return true;
            }
            return false;
        }

        public static Image GenerateThumbnail(string filePath)
        {
            var inputPhoto = Image.FromFile(filePath);
            var phWidth = inputPhoto.Width;
            var phHeight = inputPhoto.Height;

            //create a Bitmap the Size of the original photograph
            var bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(inputPhoto.HorizontalResolution, inputPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics.FromImage(bmPhoto);

            //create a image object containing the watermark
            var wmWidth = 80;
            var wmHeight = 40;


            var thumbnail = inputPhoto.GetThumbnailImage(wmWidth, wmHeight, null, IntPtr.Zero);

            //save new image to file system.
            var outputStream = new MemoryStream();
            thumbnail.Save(outputStream, GetImageFormat(filePath));
            bmPhoto.Dispose();
            thumbnail.Dispose();
            inputPhoto.Dispose();

            return Image.FromStream(outputStream);
        }

        #endregion

        #region Convert Utils
        public static bool ToBool(object value, bool defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime ToDate(object value, DateTime defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return DateTime.ParseExact(value.ToString(), "ddMMyy", null);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int ToInt(object value, int defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long ToLong(object value, long defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static float ToFloat(object value, float defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToSingle(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static double ToDouble(object value, double defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static decimal ToDecimal(object value, decimal defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToDecimal(value, new CultureInfo("en-us"));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string ToString(object value, string defaultValue)
        {
            if (value == null || value == DBNull.Value)
                return defaultValue;

            try
            {
                return Convert.ToString(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool ToBool(object value)
        {
            return ToBool(value, false);
        }

        public static DateTime ToDate(object value)
        {
            return ToDate(value, new DateTime(0));
        }

        public static int ToInt(object value)
        {
            return ToInt(value, 0);
        }

        public static long ToLong(object value)
        {
            return ToLong(value, 0);
        }

        public static float ToFloat(object value)
        {
            return ToFloat(value, 0);
        }

        public static double ToDouble(object value)
        {
            return ToDouble(value, 0);
        }

        public static decimal ToDecimal(object value)
        {
            return ToDecimal(value, 0);
        }

        public static string ToString(object value)
        {
            return ToString(value, "");
        }

        public static int SafeInt(object o, int defaultValue)
        {
            return ToInt(o, defaultValue);
        }

        public static string SafeText(string value)
        {
            if (value == null)
                return "";

            return value;
        }

        public static DateTime StringToShortDate(string value, string shortDatePattern, DateTime defaultValue)
        {
            try
            {
                var dateInfo = new DateTimeFormatInfo();

                shortDatePattern = string.IsNullOrEmpty(shortDatePattern) ? "dd/MM/yyyy" : shortDatePattern;
                dateInfo.ShortDatePattern = shortDatePattern;

                return Convert.ToDateTime(value, dateInfo);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime StringToShortDate(string value, string shortDatePattern)
        {
            return StringToShortDate(value, shortDatePattern, new DateTime(0));
        }


        public static string ConvertPriceToMoney(double price)
        {
            return price.ToString("C", CultureInfo.CreateSpecificCulture("es-US")).Replace(".00", "");
        }

        #endregion

        #region Encryption Utils

        /// <summary>
        /// Create salt key
        /// </summary>
        /// <param name="size">Key size</param>
        /// <returns>Salt key</returns>
        public static string CreateSaltKey(int size)
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Create a password hash
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="saltkey">Salk key</param>
        /// <param name="passwordFormat">Password format (hash algorithm)</param>
        /// <returns>Password hash</returns>
        public static string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            var saltAndPassword = String.Concat(password, saltkey);

            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name", "passwordFormat");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        public static string EncryptString(string str)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(str);
            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

        #endregion

        #region String Utils
        public static string GetRandomString(int len)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[len];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(5);
            foreach (var b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        /// <summary>
        /// if unit is day, value is 1 --> return 1 day.
        /// if unit is day, value is 2 --> return 2 days.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ConvertStringToMany(string unit, double value)
        {
            if (value <= 1)
                return string.Format("{0} {1}", value, unit);
            if (unit.EndsWith("s") || unit.EndsWith("ch") || unit.EndsWith("x") || unit.EndsWith("z") ||
                unit.EndsWith("sh"))
                return string.Format("{0} {1}", value, unit + "es");
            return string.Format("{0} {1}", value, unit + "s");
        }
        #endregion

        #region Locale Utils

        public static string GetCountryFromKey(string countryKey)
        {

            var ris = (from ri in
                           from ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                           select new RegionInfo(ci.LCID)
                       orderby ri.DisplayName
                       group ri by ri.TwoLetterISORegionName
                   ).FirstOrDefault(ri => ri.Key.Equals(countryKey));
            return ris == null ? string.Empty : ris.First().DisplayName;
        }




        #endregion

        #region Exception Utils

        public static string GetExceptionMessage(Exception exception)
        {
            if (exception == null)
                return string.Empty;

            while (exception.InnerException != null)
                exception = exception.InnerException;

            return exception.Message;
        }

        #endregion

        #region Math Utils

        public static double RoundUp(double value, int digits = 2)
        {
            return Math.Ceiling(value * Math.Pow(10, digits)) / Math.Pow(10, digits);
        }

        #endregion

        #region Format Utils
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
        #endregion

        #region Environment Variable

        public static string GetEnvironmentVariable(string key, EnvironmentVariableTarget target = EnvironmentVariableTarget.Machine)
        {
            return string.IsNullOrEmpty(key) ? "" : Environment.GetEnvironmentVariable(key, target);
        }

        #endregion

        #region Validate Email format

        public static bool IsEmail(string email)
        {
            return Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
        }
        #endregion

        #region Number Utils

        public static bool IsDateTime(string input)
        {
            try
            {
                Convert.ToDateTime(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsBoolean(string input)
        {
            try
            {
                Convert.ToBoolean(input);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
