using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Transverse.Models.Business.Account;
using static System.Boolean;
using static System.String;

namespace Transverse.Utils
{
    public static class BackendHelpers
    {
        #region Encryption Utils

        /// <summary>
        /// Create salt key
        /// </summary>
        /// <param name="size">Key size</param>
        /// <returns>Salt key</returns>
        public static string CreateSaltKey(int size = 15)
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
            if (IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            var saltAndPassword = Concat(password, saltkey);

            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
                throw new ArgumentException(@"Unrecognized hash name", nameof(passwordFormat));

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }

        #endregion

        #region Configurations

        public static int FormsAuthenticationCookieTimeout()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["FormsAuthenticationCookieTimeout"]);
        }

        public static string SuperAdminEmail()
        {
            return ConfigurationManager.AppSettings["SuperAdminEmail"];
        }

        public static string SuperAdminPasswordHash()
        {
            return ConfigurationManager.AppSettings["SuperAdminPasswordHash"];
        }

        public static string SuperAdminPasswordSalt()
        {
            return ConfigurationManager.AppSettings["SuperAdminPasswordSalt"];
        }

        public static string AdminEmail()
        {
            return ConfigurationManager.AppSettings["AdminEmail"];
        }

        public static string AdminPasswordHash()
        {
            return ConfigurationManager.AppSettings["AdminPasswordHash"];
        }

        public static string AdminPasswordSalt()
        {
            return ConfigurationManager.AppSettings["AdminPasswordSalt"];
        }

        public static string Email()
        {
            return ConfigurationManager.AppSettings["Email"];
        }
        public static string Password()
        {
            return ConfigurationManager.AppSettings["Password"];
        }
        public static bool EnableSsl()
        {
            return Parse(ConfigurationManager.AppSettings["EnableSsl"]);
        }
        public static string Host()
        {
            return ConfigurationManager.AppSettings["Host"];
        }
        public static int Port()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        }

	    #endregion

        public static PrincipalViewModel CurrentUser()
        {
            return HttpContext.Current.User as PrincipalViewModel;
        }

        public static int CurrentUserId()
        {
            return CurrentUser().Id;
        }

        public static string CurrentUserName()
        {
            return CurrentUser().FirstName + " " + CurrentUser().LastName;
        }

        public static string CurrentUserEmail()
        {
            return CurrentUser().Email;
        }

        public static string CurrentUserRole()
        {
            return CurrentUser().Role;
        }

        public static bool CurrentUserIsInRole(string role)
        {
            return CurrentUser().IsInRole(role);
        }
    }
}