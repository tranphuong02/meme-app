using System.Collections.Generic;
using Core.Common;

namespace Core.Validator
{
    public class ValidatorResource
    {
        private static Dictionary<string, string> _cache = new Dictionary<string, string>();

        public static string email_error
        {
            get
            {
                return GetMessange("email_error");
            }
        }
        public static string equal_error
        {
            get
            {
                return GetMessange("equal_error");
            }
        }
        public static string exact_length_error
        {
            get
            {
                return GetMessange("exact_length_error");
            }
        }
        public static string exclusivebetween_error
        {
            get
            {
                return GetMessange("exclusivebetween_error");
            }
        }
        public static string greaterthan_error
        {
            get
            {
                return GetMessange("greaterthan_error");
            }
        }
        public static string greaterthanorequal_error
        {
            get
            {
                return GetMessange("greaterthanorequal_error");
            }
        }
        public static string inclusivebetween_error
        {
            get
            {
                return GetMessange("inclusivebetween_error");
            }
        }
        public static string length_error
        {
            get
            {
                return GetMessange("length_error");
            }
        }
        public static string lessthan_error
        {
            get
            {
                return GetMessange("lessthan_error");
            }
        }
        public static string lessthanorequal_error
        {
            get
            {
                return GetMessange("lessthanorequal_error");
            }
        }
        public static string notempty_error
        {
            get
            {
                return GetMessange("notempty_error");
            }
        }
        public static string notequal_error
        {
            get
            {
                return GetMessange("notequal_error");
            }
        }
        public static string notnull_error
        {
            get
            {
                return GetMessange("notnull_error");
            }
        }
        public static string predicate_error
        {
            get
            {
                return GetMessange("predicate_error");
            }
        }
        public static string regex_error
        {
            get
            {
                return GetMessange("regex_error");
            }
        }
        public static string existed
        {
            get
            {
                return GetMessange("existed");
            }
        }
        public static string notexisted
        {
            get
            {
                return GetMessange("notexisted");
            }
        }
        public static string notcontainspaceandspecialcharacters
        {
            get
            {
                return GetMessange("notcontainspaceandspecialcharacters");
            }
        }
        public static string notcontainspace
        {
            get
            {
                return GetMessange("notcontainspace");
            }
        }
        public static string notcontaintspecialcharacters
        {
            get
            {
                return GetMessange("notcontainspecialcharacters");
            }
        }
        public static string mustbenumberic
        {
            get
            {
                return GetMessange("mustbenumberic");
            }
        }
        public static string notcontainnumberic
        {
            get
            {
                return GetMessange("notcontainnumberic");
            }
        }
        public static string mustphonefax
        {
            get
            {
                return GetMessange("mustphonefax");
            }
        }
        public static string password_incorect
        {
            get
            {
                return GetMessange("password_incorect");
            }
        }

        private static string GetMessange(string pPropertyName)
        {
            if (!_cache.ContainsKey(pPropertyName))
            {
                _cache.Add(pPropertyName, CoreCommon.GetStringResource("fluentvalidation_" + pPropertyName));
            }
            return _cache[pPropertyName];
        }
    }
}
