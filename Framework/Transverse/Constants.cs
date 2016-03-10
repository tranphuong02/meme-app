using System;
using System.Configuration;
using System.IO;

namespace Transverse
{
    public static class Constants
    {
        public static readonly int MaxFileSize = 4 * 1000 * 1024;

        public static readonly string MaxFileSizeErroMessage = "Maximum file size allow is 4MB";

        public const string DefaultImage = "Content/Images/def_img.png";

        public const int AllValue = -1;

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
            /// <summary>
            /// Define the success to create message
            /// </summary>
            public const string SuccessToCreate = "This {0} has been created successfully";

            /// <summary>
            /// Define the fail to create message
            /// </summary>
            public const string FailToCreate = "This {0} has been create unsuccessfully";

            /// <summary>
            /// Define the success to edit message
            /// </summary>
            public const string SuccessToEdit = "This {0} has been edited successfully";

            /// <summary>
            /// Define the fail to edit message
            /// </summary>
            public const string FailToEdit = "This {0} has been edited unsuccessfully";

            /// <summary>
            /// Define the success to delete message
            /// </summary>
            public const string SuccessToDelete = "This {0} has been deleted successfully";

            /// <summary>
            /// Define the fail to delete message
            /// </summary>
            public const string FailToDelete = "This {0} has been deleted unsuccessfully";

            /// <summary>
            /// Define the success to approve message
            /// </summary>
            public const string SuccessToApprove = "This {0} has been approved successfully";

            /// <summary>
            /// Define the fail to approve message
            /// </summary>
            public const string FailToApprove = "This {0} has been approved unsuccessfully";

            /// <summary>
            /// Define the success to approve message
            /// </summary>
            public const string SuccessToReject = "This {0} has been rejected successfully";

            /// <summary>
            /// Define the fail to approve message
            /// </summary>
            public const string FailToReject = "This {0} has been rejected unsuccessfully";

            /// <summary>
            /// Define the success to re-open message
            /// </summary>
            public const string SuccessToReOpen = "This {0} has been re-opened successfully";

            /// <summary>
            /// Define the fail to re-open message
            /// </summary>
            public const string FailToReOpen = "This {0} has been re-opened unsuccessfully";

            /// <summary>
            /// Define the success to re-open message
            /// </summary>
            public const string SuccessToArchive = "This {0} has been archived successfully";

            /// <summary>
            /// Define the fail to re-open message
            /// </summary>
            public const string FailToArchive = "This {0} has been archived unsuccessfully";

            /// <summary>
            /// Define the success to re-open message
            /// </summary>
            public const string SuccessToActive = "This {0} has been active successfully";

            /// <summary>
            /// Define the fail to re-open message
            /// </summary>
            public const string FailToActive = "This {0} has been active unsuccessfully";

            /// <summary>
            /// Define the error occur  message
            /// </summary>
            public const string ErrorOccur = "Error occur, please try again!";

            /// <summary>
            /// Define the is not exists message
            /// </summary>
            public const string IsNotExists = "{0} does not exist";

            /// <summary>
            /// Define the exists message
            /// </summary>
            public const string IsExists = "{0} is exist";

            /// <summary>
            /// Define the invalid status message for deal/coupon
            /// </summary>
            public const string IvalidStatus = "Invalid status. Your {0} {1} was edited by another checker or manager. Please refresh page and try again!";
        }

        public class RoleName
        {
            public const string SupperAdmin = "SupperAdmin";
            public const string Admin = "Admin";
            public const string Mod = "Mod";
            public const string Writer = "Writer";
        }
    }
}