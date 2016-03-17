using System;
using System.Configuration;
using System.IO;

namespace Transverse
{
    public static class Constants
    {
        public const string AppName = "Meme App";

        public static readonly int MaxFileSize = 4 * 1000 * 1024;

        public static readonly string MaxFileSizeErroMessage = "Maximum file size allow is 4MB";

        public const string DefaultImage = "Content/Images/def_img.png";

        public const string AllText = "All";

        public const int AllValue = -1;

        public const string AdminArea = "Administrator";

        public const string DefaultPassword = "pass1234";

        public const string AdminLayout = "~/Areas/Administrator/Views/Shared/_AdminLayout.cshtml";

        public const string NologinLayout = "~/Areas/Administrator/Views/Shared/_NologinLayout.cshtml";

        public static class ResourcePath
        {
            public static readonly string AppDataFolder = (string)AppDomain.CurrentDomain.GetData("DataDirectory") ?? AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            public static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            public static readonly string Resource = Path.Combine(BaseDirectory, ConfigurationManager.AppSettings["ResourceDirectory"]);
        }

        /// <summary>
        /// Status code
        /// </summary>
        public class StatusCode
        {
            /// <summary>
            /// Success
            /// </summary>
            public const string Success = "0";

            /// <summary>
            /// Fail
            /// </summary>
            public const string Fail = "1";
        }

        /// <summary>
        /// Error Code
        /// </summary>
        public static class ErrorCode
        {
            /// <summary>
            /// Bad request
            /// </summary>
            public const string BadRequest = "400";
        }

        public class Message
        {
            public const string SuccessToAdd = "This {0} has been added successfully";

            public const string SuccessToEdit = "This {0} has been edited successfully";

            public const string SuccessToDelete = "This {0} has been deleted successfully";

            public const string SuccessToApprove = "This {0} has been approved successfully";

            public const string SuccessToReject = "This {0} has been rejected successfully";

            public const string SuccessToReOpen = "This {0} has been re-opened successfully";

            public const string SuccessToArchive = "This {0} has been archived successfully";

            public const string SuccessToActive = "This {0} has been active successfully";

            public const string SuccessToDeactive = "This {0} has been deactive successfully";

            public const string SuccessToResetPassword = "This {0} has been reset successfully";

            public const string SuccessToChangePassword = "Your password has been changed successfully";

            public const string ErrorOccur = "Error occur, please try again!";

            public const string IsNotExists = "This {0} does not exist";

            public const string IsExists = "{0} is exist";

            public const string IvalidStatus = "Invalid status. Your {0} {1} was edited by another checker or manager. Please refresh page and try again!";

            public const string InvalidLogin = "Username and password is invalid.";

            public const string NoData = "There is no {0} data";


        }

        public class RoleName
        {
            public const string SuperAdmin = "SuperAdmin";
            public const string Admin = "Admin";
            public const string Moderator = "Moderator";
            public const string User = "User";
            public const string Customer = "Customer";
        }
    }
}