using System.Collections.Generic;

namespace Common.Enums
{
    namespace CommonEnums
    {
        public enum CustomImageFormat
        {
            Png,
            Jpg,
            Jpeg,
            Bmp,
            Gif,
            Tiff,
            Unknown
        }

        public enum Gender
        {
            Male = 1,
            Female,
            Other
        }

        public enum TabsEnum
        {
            Profile = 0,
            BasicInfo,
            Password
        }

        public enum StatusMessageType
        {
            Success,
            Info,
            Warning,
            Danger
        }

    }

    public static class EnumUtils
    {
        public static readonly string[] ImageExtensions = new string[6]
        {
            ".png",
            ".jpg",
            ".jpeg",
            ".bmp",
            ".gif",
            ".tiff"
        };

        public static List<string> VideoExtensions = new List<string>()
        {
            ".mp4", ".avi",".wmv",".flv"
        };

        public static Dictionary<string, int> GenderList
        {
            get
            {
                return new Dictionary<string, int>
                {
                    {"Male", (int) Gender.Male},
                    {"Female", (int) Gender.Female},
                    {"Other", (int) Gender.Other}
                };
            }
        }
    }
}
