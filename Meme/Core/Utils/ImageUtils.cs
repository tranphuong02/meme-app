using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Core.Utils
{
    public static class ImageUtils
    {
        private static readonly string[] ImageExtensions = new string[6]
        {
            ".bmp",
            ".jpg",
            ".jpeg",
            ".gif",
            ".tiff",
            ".png"
        };

        public enum MyImageFormat
        {
            bmp,
            jpeg,
            gif,
            tiff,
            png,
            unknown
        }

        public static string ObjectToJSONString(Object obj)
        {
            var jsSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.None, jsSettings);
            return jsonString;
        }


        public static MyImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return MyImageFormat.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return MyImageFormat.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return MyImageFormat.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return MyImageFormat.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return MyImageFormat.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return MyImageFormat.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return MyImageFormat.jpeg;

            return MyImageFormat.unknown;
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
            return ImageFormat.Jpeg;
        }

        public static bool IsImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;
            fileName = fileName.ToLower();
            foreach (var extension in ImageExtensions)
            {
                if (fileName.EndsWith(extension))
                    return true;
            }
            return false;
        }

        public static Image GenerateThumbnail(string filePath)
        {
            var inputPhoto = Bitmap.FromFile(filePath);
            var phWidth = inputPhoto.Width;
            var phHeight = inputPhoto.Height;

            //create a Bitmap the Size of the original photograph
            var bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(inputPhoto.HorizontalResolution, inputPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            var grPhoto = Graphics.FromImage(bmPhoto);

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

        public static string ToStringWithDash(this string input)
        {
            input = input.Replace("&", "");
            if (string.IsNullOrEmpty(input))
                return string.Empty;
            while (input.IndexOf("  ") != -1)
            {
                input = input.Replace("  ", " ");
            }
            input = input.ToLower();
            return input.Replace(" ", "-");
        }

        public static string ToUrlWithDash(string[] strs)
        {
            return strs.Aggregate(string.Empty, (current, str) => current + ("/" + str.ToStringWithDash().Trim()));
        }
    }
}
