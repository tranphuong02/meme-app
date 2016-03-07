using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Framework.Utility
{
    public static class FileHelper
    {
        public static void ByteArrayToFile(this Byte[] byteArray, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Open file for reading
            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            // Writes a block of bytes to this stream using data from
            // a byte array.
            fileStream.Write(byteArray, 0, byteArray.Length);

            // close file stream
            fileStream.Close();
        }

        public static void StringBuilderToFile(this StringWriter stringWriter, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            byte[] preamble = Encoding.UTF8.GetPreamble();

            byte[] bufferData = Encoding.UTF8.GetBytes(stringWriter.ToString());
            //byte[] buffer = preamble.Concat(bufferData).ToArray();
            byte[] buffer = Combine(preamble, bufferData);

            // Open file for reading
            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            // Writes a block of bytes to this stream using data from
            // a byte array.
            fileStream.Write(buffer, 0, buffer.Length);

            // close file stream
            fileStream.Close();
        }

        private static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}