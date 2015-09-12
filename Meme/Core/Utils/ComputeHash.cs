using System;

namespace Core.Utils
{
    public class ComputeHash
    {
        /// <summary>
        /// this to compute a hash int number for a string to simplify and speedup string search
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int64 GetHashLongValue(string str)
        {
            Int64 result = 0;
            var mod = str.Length % 8;
            for (int i = 0; i < str.Length; )
            {
                var tempBytes = new byte[8];
                if (str.Length - i >= 8)
                {
                    for (int j = 0; j < 8; )
                    {
                        tempBytes[7 - j] = (byte)str[i];
                        j++;
                        i++;
                    }
                }
                else
                {
                    for (int j = 0; j < mod; )
                    {
                        tempBytes[7 - j] = (byte)str[i];
                        j++;
                        i++;
                    }
                }
                result ^= BitConverter.ToInt64(tempBytes, 0);
            }
            return result;
        }

        /// <summary>
        /// this to compute a hash int number for a string to simplify and speedup string search
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int32 GetHashIntValue(string str)
        {
            Int32 result = 0;
            var mod = str.Length % 4;
            for (int i = 0; i < str.Length; )
            {
                var tempBytes = new byte[4];
                if (str.Length - i >= 4)
                {
                    for (int j = 0; j < 4; )
                    {
                        tempBytes[3 - j] = (byte)str[i];
                        j++;
                        i++;
                    }
                }
                else
                {
                    for (int j = 0; j < mod; )
                    {
                        tempBytes[3 - j] = (byte)str[i];
                        j++;
                        i++;
                    }
                }
                result ^= BitConverter.ToInt32(tempBytes, 0);
            }
            return result;
        }
    }
}
