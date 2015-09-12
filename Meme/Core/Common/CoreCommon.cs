using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common
{
    public class CoreCommon
    {
        public static string GetStringResource(string pResourceID)
        {
            string message = Properties.Resources.ResourceManager.GetString(pResourceID);

            return message;
        }
    }
}
